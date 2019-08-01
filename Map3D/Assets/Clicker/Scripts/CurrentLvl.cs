using UnityEngine;
using UnityEngine.UI;

public class CurrentLvl : MonoBehaviour
{
    [SerializeField] private Text lvlText;

    private ClickerManager _gameManager;

    public CurrentLvl Init(ClickerManager gameManager)
    {
        _gameManager = gameManager;
        return this;
    }

    void Update()
    {
        lvlText.text = "Lvl " + _gameManager.lvl;
    }
}