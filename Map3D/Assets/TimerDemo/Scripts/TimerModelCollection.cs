using System;
using UnityEngine;

namespace TimerComponents
{
    [Serializable]
    public class TimerModelCollection
    {
        [SerializeField]
        private TimerModel[] _timerModels;

        public TimerModelCollection(TimerModel[] timerModels)
        {
            _timerModels = timerModels;
        }

        public TimerModel[] TimerModels => _timerModels;
    }
}