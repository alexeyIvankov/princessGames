using System;
using UnityEngine;

namespace TimerComponents
{
    [Serializable]
    public class TimeModel
    {
        [SerializeField]
        private int _days;
        [SerializeField]
        private int _hours;
        [SerializeField]
        private int _minutes;
        [SerializeField]
        private int _seconds;

        public int Days => _days;

        public int Hours => _hours;

        public int Minutes => _minutes;
        
        public int Seconds => _seconds;

        public TimeModel(int days, int hours, int minutes, int seconds)
        {
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }
    }
}