using System;
using System.Collections.Generic;
using TimerComponents;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private Dictionary<string, TimerModel> _timeModels = new Dictionary<string, TimerModel>();

    [SerializeField] private GameObject _timer;

    public Image MainCanvas;

    public static TimerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Dictionary<string, TimerModel> models = StorageController.RestoreDataFromStorage();

        var prefab = _timer;
        var newTimer = Instantiate(prefab);
        newTimer.transform.SetParent(MainCanvas.transform, false);
        
        foreach (var timeModelsValue in models.Values)
        {
            _timeModels.Add(timeModelsValue.Name, timeModelsValue);
            prefab = _timer;
            newTimer = Instantiate(prefab);
            newTimer.transform.SetParent(MainCanvas.transform, false);
            var timerScript = newTimer.GetComponent<Timer>();
            timerScript.SetStartValues(timeModelsValue);
            
        }
    }

    public bool OnCreateTimerClick(string timerName, TimerModel timerModel)
    {
        if (_timeModels.ContainsKey(timerName))
        {
            
           // EditorUtility.DisplayDialog("Имя занято", "Данное имя используется для другого таймера.", "Переименовать");
            return false;
        }
        _timeModels.Add(timerName, timerModel);
        var prefab = _timer;
        var newTimer = Instantiate(prefab);
        newTimer.transform.SetParent(MainCanvas.transform, false);
        UpdateDataInStorage();
        return true;
    }

    public bool OnTimerRename(string oldTimerName, string newTimerName)
    {
        if (_timeModels.ContainsKey(newTimerName))
        {
          //  EditorUtility.DisplayDialog("Имя занято", "Данное имя используется для другого таймера.", "Переименовать");
            return false;
        }
        var timeModel = _timeModels[oldTimerName];
        _timeModels.Remove(oldTimerName);
        _timeModels.Add(newTimerName, timeModel);
        UpdateDataInStorage();
        return true;
    }

    public void OnTimerForceUpdate(string timerName, TimerModel timerModel)
    {
        _timeModels[timerName] = timerModel;
        UpdateDataInStorage();
    }

    public void OnTimerDelete(string timerName, TimerModel timerModel)
    {
        _timeModels.Remove(timerName);
        UpdateDataInStorage();
    }

    private void UpdateDataInStorage()
    {
        StorageController.UpdateDataInStorage(_timeModels);
    }
}