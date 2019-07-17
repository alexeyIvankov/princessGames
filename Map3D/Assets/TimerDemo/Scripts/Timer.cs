using System;
using System.Collections;
using System.Text.RegularExpressions;
using TimerComponents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timer : MonoBehaviour, IDragHandler
{
    [SerializeField] private InputField _timerName;
    [SerializeField] private InputField _daysText;
    [SerializeField] private InputField _hoursText;
    [SerializeField] private InputField _minutesText;
    [SerializeField] private InputField _secondsText;
    private static int _timerIndex;
    private int _days;
    private int _hours;
    private int _minutes;
    private int _seconds;
    private const int MaxHours = 24;
    private const int MaxMinutes = 60;
    private const int MaxSeconds = 60;

    private TimeModel _timeInitial;

    private string _savedTimerName="";

    private bool _onPause;

    [SerializeField] private Button _createTimerButton;
    [SerializeField] private Button _startTimerButton;
    [SerializeField] private Button _pauseTimerButton;
    [SerializeField] private Button _deleteTimerButton;
    [SerializeField] private Button _editTimerButton;
    [SerializeField] private Button _saveTimerButton;
    [SerializeField] private Button _resetTimerButton;

    [SerializeField] private GameObject _timerPanel;

    private IEnumerator _timerCoroutine;


    private IEnumerator GetTimerRoutine()
    {
        if (_timerCoroutine == null)
        {
            _timerCoroutine = WaitForFinishTimer();
        }

        return _timerCoroutine;
    }

    private void Start()
    {
        if (_savedTimerName=="")
        {
            _startTimerButton.gameObject.SetActive(false);
            _pauseTimerButton.gameObject.SetActive(false);
            _deleteTimerButton.gameObject.SetActive(false);
            _editTimerButton.gameObject.SetActive(false);
            _saveTimerButton.gameObject.SetActive(false);
            _resetTimerButton.gameObject.SetActive(false);
            _timerName.text = $"Timer{_timerIndex++}";
        }

        _daysText.onValidateInput += (input, charIndex, addedChar) => TimeInputValidation(addedChar);
        _hoursText.onValidateInput += (input, charIndex, addedChar) => TimeInputValidation(addedChar);
        _minutesText.onValidateInput += (input, charIndex, addedChar) => TimeInputValidation(addedChar);
        _secondsText.onValidateInput += (input, charIndex, addedChar) => TimeInputValidation(addedChar);
    }

    public void SetStartValues(TimerModel model)
    {
        _timeInitial = model.TimeInitial;
        _savedTimerName = _timerName.text = model.Name;
        ControlValidNameGeneration();
        
        if (model.Paused)
        {
            _onPause = true;
            _days = model.TimeLeft.Days;
            _hours = model.TimeLeft.Hours;
            _minutes = model.TimeLeft.Minutes;
            _seconds = model.TimeLeft.Seconds;
            _daysText.text = model.TimeLeft.Days.ToString();
            _hoursText.text = model.TimeLeft.Hours.ToString();
            _minutesText.text = model.TimeLeft.Minutes.ToString();
            _secondsText.text = model.TimeLeft.Seconds.ToString();
            _createTimerButton.gameObject.SetActive(false);
            _startTimerButton.gameObject.SetActive(true);
            _pauseTimerButton.gameObject.SetActive(false);
            _deleteTimerButton.gameObject.SetActive(true);
            _editTimerButton.gameObject.SetActive(true);
            _resetTimerButton.gameObject.SetActive(true);
            _saveTimerButton.gameObject.SetActive(false);
            _timerName.enabled = false;
            _daysText.enabled = false;
            _hoursText.enabled = false;
            _minutesText.enabled = false;
            _secondsText.enabled = false;
        }
        else
        {
            var diffDateTime = TimeSpan.FromTicks(DateTime.Now.ToFileTimeUtc() - model.LastUpdate);
            var days = model.TimeLeft.Days - diffDateTime.Days;
            var hours = model.TimeLeft.Hours - diffDateTime.Hours;
            var minutes = model.TimeLeft.Minutes - diffDateTime.Minutes;
            var seconds = model.TimeLeft.Seconds - diffDateTime.Seconds;
            if (seconds < 0)
            {
                minutes--;
                seconds = MaxSeconds + seconds;
            }

            if (minutes < 0)
            {
                hours--;
                minutes = MaxMinutes + minutes;
            }
            if (hours < 0)
            {
                days--;
                hours = MaxHours + hours;
            }
            if (days < 0)
            {
                _days = _hours = _minutes = _seconds = 0;
            }
            else
            {
                _seconds = seconds;
                _minutes = minutes;
                _hours = hours;
                _days = days;
            }
            _daysText.text = _days.ToString();
            _hoursText.text = _hours.ToString();
            _minutesText.text = _minutes.ToString();
            _secondsText.text = _seconds.ToString();
            
            _createTimerButton.gameObject.SetActive(false);
            _pauseTimerButton.gameObject.SetActive(true);
            _deleteTimerButton.gameObject.SetActive(true);
            _editTimerButton.gameObject.SetActive(true);
            _saveTimerButton.gameObject.SetActive(false);
            _resetTimerButton.gameObject.SetActive(true);
            _timerName.enabled = false;
            _daysText.enabled = false;
            _hoursText.enabled = false;
            _minutesText.enabled = false;
            _secondsText.enabled = false;
            _onPause = false;
            StartCoroutine(GetTimerRoutine());
        }
    }

    public void OnCreateClick()
    {
        int.TryParse(_daysText.text, out _days);
        int.TryParse(_hoursText.text, out _hours);
        int.TryParse(_minutesText.text, out _minutes);
        int.TryParse(_secondsText.text, out _seconds);
        if (_days == 0 && _hours == 0 && _minutes == 0 && _seconds == 0)
        {
            _minutes = 10;
        }
        _timeInitial = new TimeModel(_days, _hours, _minutes, _seconds);
        _savedTimerName = _timerName.text;
        ControlValidNameGeneration();

        if (!TimerController.Instance.OnCreateTimerClick(_savedTimerName,
            new TimerModel(_timeInitial, _timeInitial, _savedTimerName, false)))
        {
            return;
        }
        StartCoroutine(GetTimerRoutine());
        
        _createTimerButton.gameObject.SetActive(false);
        _pauseTimerButton.gameObject.SetActive(true);
        _deleteTimerButton.gameObject.SetActive(true);
        _editTimerButton.gameObject.SetActive(true);
        _resetTimerButton.gameObject.SetActive(true);
        _timerName.enabled = false;
        _daysText.enabled = false;
        _hoursText.enabled = false;
        _minutesText.enabled = false;
        _secondsText.enabled = false;
        _onPause = false;
    }

    private IEnumerator WaitForFinishTimer()
    {
        while (_days > 0 || _hours > 0 || _minutes > 0 || _seconds > 0)
        {
            yield return new WaitForSeconds(1f);
            UpdateTimer();
        }

        _daysText.text = _timeInitial.Days.ToString();
        _hoursText.text = _timeInitial.Hours.ToString();
        _minutesText.text = _timeInitial.Minutes.ToString();
        _secondsText.text = _timeInitial.Seconds.ToString();
        _startTimerButton.gameObject.SetActive(true);
        _pauseTimerButton.gameObject.SetActive(false);
        _editTimerButton.gameObject.SetActive(false);
        _saveTimerButton.gameObject.SetActive(true);
        _resetTimerButton.gameObject.SetActive(false);
        _onPause = true;
        OnResetClick();
    }

    private void UpdateTimer()
    {
        //for seconds
        if (_seconds > 0)
        {
            _seconds--;
        }
        else
        {
            //for minutes
            _seconds = MaxSeconds - 1;
            if (_minutes > 0)
            {
                _minutes--;
            }
            else
            {
                //for hours
                _minutes = MaxMinutes - 1;
                if (_hours > 0)
                {
                    _hours--;
                }
                else
                {
                    //for days
                    _hours = MaxHours - 1;
                    if (_days > 0)
                    {
                        _days--;
                    }

                    _daysText.text = $"{_days}";
                }

                _hoursText.text = $"{_hours}";
            }

            _minutesText.text = $"{_minutes}";
        }

        _secondsText.text = $"{_seconds}";
    }

    private static char TimeInputValidation(char input)
    {
        return !char.IsDigit(input) ? '\0' : input;
    }

    public void OnHoursChange()
    {
        if (_hoursText.text == "")
        {
            return;
        }

        int.TryParse(_hoursText.text, out var hours);
        hours = hours > MaxHours ? MaxHours : hours;
        _hoursText.text = $"{hours}";
    }

    public void OnMinutesChange()
    {
        if (_minutesText.text == "")
        {
            return;
        }

        if (!int.TryParse(_minutesText.text, out var minutes)) return;
        minutes = minutes > MaxMinutes ? MaxMinutes : minutes;
        _minutesText.text = $"{minutes}";
    }

    public void OnSecondsChange()
    {
        if (_secondsText.text == "")
        {
            return;
        }

        int.TryParse(_secondsText.text, out var seconds);
        seconds = seconds > MaxSeconds ? MaxSeconds : seconds;
        _secondsText.text = $"{seconds}";
    }

    public void OnStartClick()
    {
        _onPause = false;
        int.TryParse(_daysText.text, out _days);
        int.TryParse(_hoursText.text, out _hours);
        int.TryParse(_minutesText.text, out _minutes);
        int.TryParse(_secondsText.text, out _seconds);
        StartCoroutine(GetTimerRoutine());
        var model = new TimerModel(new TimeModel(_days, _hours, _minutes, _seconds), _timeInitial, _savedTimerName, false);
        TimerController.Instance.OnTimerForceUpdate(_timerName.text, model);
        _startTimerButton.gameObject.SetActive(false);
        _resetTimerButton.gameObject.SetActive(true);
        _pauseTimerButton.gameObject.SetActive(true);

    }

    public void OnPauseClick()
    {
        _onPause = true;
        StopCoroutine(GetTimerRoutine());

        var model = new TimerModel(new TimeModel(_days, _hours, _minutes, _seconds), _timeInitial, _savedTimerName, true);
        TimerController.Instance.OnTimerForceUpdate(_timerName.text, model);
        _startTimerButton.gameObject.SetActive(true);
        _pauseTimerButton.gameObject.SetActive(false);
    }

    public void OnEditClick()
    {
        if (!_onPause)
        {
            StopCoroutine(GetTimerRoutine());
            _onPause = true;
            var model = new TimerModel(new TimeModel(_days, _hours, _minutes, _seconds), _timeInitial, _savedTimerName, true);
            TimerController.Instance.OnTimerForceUpdate(_timerName.text, model);
        }

        _timerName.enabled = true;
        _daysText.enabled = true;
        _hoursText.enabled = true;
        _minutesText.enabled = true;
        _secondsText.enabled = true;

        _startTimerButton.gameObject.SetActive(false);
        _pauseTimerButton.gameObject.SetActive(false);
        _editTimerButton.gameObject.SetActive(false);
        _saveTimerButton.gameObject.SetActive(true);
    }

    public void OnSaveClick()
    {
        if (_savedTimerName != _timerName.text)
        {
            if (!TimerController.Instance.OnTimerRename(_savedTimerName, _timerName.text))
            {
                return;
            }
            _savedTimerName = _timerName.text;
            ControlValidNameGeneration();
        }

        int.TryParse(_daysText.text, out _days);
        int.TryParse(_hoursText.text, out _hours);
        int.TryParse(_minutesText.text, out _minutes);
        int.TryParse(_secondsText.text, out _seconds);

        _timerName.enabled = false;
        _daysText.enabled = false;
        _hoursText.enabled = false;
        _minutesText.enabled = false;
        _secondsText.enabled = false;
        
        _timeInitial = new TimeModel(_days, _hours, _minutes, _seconds);

        var model = new TimerModel(_timeInitial, _timeInitial, _savedTimerName, true);
        TimerController.Instance.OnTimerForceUpdate(_timerName.text, model);
        _startTimerButton.gameObject.SetActive(true);
        _editTimerButton.gameObject.SetActive(true);
        _saveTimerButton.gameObject.SetActive(false);
    }

    private void ControlValidNameGeneration()
    {
        if (_timerName.text.Substring(0, 5) != "Timer" ||
            !int.TryParse(_timerName.text.Substring(5), out var curIndex)) return;
        if (_timerIndex < curIndex)
        {
            _timerIndex = curIndex + 1;
        }
    }
    
    public void OnResetClick()
    {
        if (!_onPause)
        {
            StopCoroutine(GetTimerRoutine());
            _onPause = true;
            _startTimerButton.gameObject.SetActive(true);
            _pauseTimerButton.gameObject.SetActive(false);
        }

        _daysText.text = _timeInitial.Days.ToString();
        _hoursText.text = _timeInitial.Hours.ToString();
        _minutesText.text = _timeInitial.Minutes.ToString();
        _secondsText.text = _timeInitial.Seconds.ToString();

        var model = new TimerModel(_timeInitial, _timeInitial, _savedTimerName, true);
        TimerController.Instance.OnTimerForceUpdate(_timerName.text, model);
        _startTimerButton.gameObject.SetActive(true);
        _editTimerButton.gameObject.SetActive(true);
        _saveTimerButton.gameObject.SetActive(false);
        _timerCoroutine = null;
    }

    public void OnDeleteClick()
    {
        if (!_onPause)
        {
            StopCoroutine(GetTimerRoutine());
        }

        var timeModel = new TimeModel(_days, _hours, _minutes, _seconds);
        TimerController.Instance.OnTimerDelete(_timerName.text, new TimerModel(timeModel, _timeInitial, _savedTimerName, true));
        Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _timerPanel.transform.position = Input.mousePosition;
    }
}