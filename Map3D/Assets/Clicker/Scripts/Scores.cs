using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private ClickerManager _gameManager;

    public Scores Init(ClickerManager gameManager)
    {
        _gameManager = gameManager;
        return this;
    }

    void Update()
    {
        scoreText.text = _gameManager.Timer.currentScores.ToString();
    }
}