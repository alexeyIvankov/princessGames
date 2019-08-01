using System;
using UnityEngine.UI;
using UnityEngine;

public class Timer1 : MonoBehaviour
{
    [SerializeField] public Text timerText;
    public int bonusTime;
    private ClickerManager _gameManager;
    public int currentScores;
    public float StartTime { get; set; }

    public void Start()
    {
        StartTime = 10;
    }

    public Timer1 Init(ClickerManager gameManager)
    {
        _gameManager = gameManager;
        return this;
    }

    public void Update()
    {
        if (_gameManager)
        {
            var cooldown = StartTime - Time.timeSinceLevelLoad * 1.5f + bonusTime;
            if (_gameManager.hit)
            {
                bonusTime += 4;
                currentScores += 10;
                _gameManager.hit = false;
            }

            if (cooldown <= 0)
            {
                _gameManager.FinishGame(currentScores);
            }

            String seconds = (cooldown % 60).ToString("F0");
            timerText.text = seconds;
        }
    }
}