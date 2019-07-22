using System;
using UnityEngine;

namespace TimerComponents
{
    [Serializable]
    public class TimerModel
    {
        [SerializeField]
        private TimeModel _timeLeft;
        [SerializeField]
        private TimeModel _timeInitial;
        [SerializeField]
        private bool _paused;
        [SerializeField]
        private long _lastUpdate;
        [SerializeField]
        private string _name;

        public TimeModel TimeLeft => _timeLeft;

        public TimeModel TimeInitial => _timeInitial;

        public bool Paused => _paused;

        public long LastUpdate => _lastUpdate;

        public string Name => _name;

        public TimerModel(TimeModel timeLeft, TimeModel timeInitial, string name, bool paused)
        {
            _timeLeft = timeLeft;
            _timeInitial = timeInitial;
            _paused = paused;
            _lastUpdate = DateTime.Now.ToFileTimeUtc();
            _name = name;
        }
    }
}