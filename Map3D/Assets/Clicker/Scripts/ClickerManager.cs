using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClickerManager : MonoBehaviour
{
    [SerializeField] private CreateRay ray;
    [SerializeField] private Ring ring;
    [SerializeField] private Laser laser;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Text _winText;
    [SerializeField] private Text _scores;


    public bool hit;
    private bool _frezee;
    public float Velocity { get; private set; }

    public Timer1 Timer { get; set; }

    public Scores Score { get; set; }

    public CurrentLvl CurrentLvl { get; set; }

    public Laser Laser { get; set; }

    private CreateRay _ray;
    public int lvl;

    public List<Color> colors = new List<Color>
    {
        Color.red
    };

    private List<Color> notUsedColors;
    public List<Ring> existingRings = new List<Ring>();
    public Color rayColor;


    private void Start()
    {
        Laser = Instantiate(laser).Init();
        existingRings.Add(BigRing());
        Velocity = 1;
        notUsedColors = new List<Color>(colors);
        UpdateColor();
        lvl = 1;
        Debug.Log("WE will work with color: " + rayColor);
        _frezee = false;
        hit = false;

        Timer = GetComponent<Timer1>().Init(this);
        Score = GetComponent<Scores>().Init(this);
        CurrentLvl = GetComponent<CurrentLvl>().Init(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_frezee)

        {
            _ray = Instantiate(ray).Init(this);
        }

        //Debug.Log("VELOSITY", Velocity.ToString());
    }

    private Ring BigRing()
    {
        Ring bigRing = Instantiate(ring);
        bigRing.r1 = 20;
        bigRing.r2 = 23;
        bigRing.GameManager = this;
        bigRing.Init(Color.red);
        return bigRing;
    }

    private Ring MeanRing()
    {
        Ring meanRing = Instantiate(ring);
        meanRing.r1 = 15;
        meanRing.r2 = 18;
        meanRing.GameManager = this;
        meanRing.Init(Color.blue);
        return meanRing;
    }

    private Ring SmallRing()
    {
        Ring smallRing = Instantiate(ring);
        smallRing.r1 = 10;
        smallRing.r2 = 13;
        smallRing.GameManager = this;
        smallRing.Init(Color.green);
        return smallRing;
    }

    private Color GetRandomColor()
    {
        var random = Random.Range(0, notUsedColors.Count - 1);
        var chosenColor = notUsedColors[random];
        notUsedColors.Remove(chosenColor);
        return chosenColor;
    }

    public void FinishGame(int scores)
    {
        _winPanel.SetActive(true);
        _winText.text = "GAME OVER";
        _scores.text = "Scores: " + scores;
        _frezee = true;
    }

    public void DeleteRing(Ring ring)
    {
        existingRings.Remove(ring);
        ring.Delete();
        hit = true;
        InitLevels();
    }

    public void InitLevels()
    {
        if (existingRings.Count == 0)
        {
            ++lvl;
            switch (colors.Count)
            {
                case 1:
                {
                    colors.Add(Color.blue);
                    notUsedColors = new List<Color>(colors);
                    existingRings.Add(BigRing());
                    existingRings.Add(MeanRing());
                    break;
                }

                case 2:
                {
                    colors.Add(Color.green);
                    notUsedColors = new List<Color>(colors);
                    existingRings.Add(BigRing());
                    existingRings.Add(MeanRing());
                    existingRings.Add(SmallRing());
                    break;
                }

                case 3:
                {
                    notUsedColors = new List<Color>(colors);
                    Velocity = Velocity * 1.25f;
                    existingRings.Add(BigRing());
                    existingRings.Add(MeanRing());
                    existingRings.Add(SmallRing());
                    break;
                }
            }
        }
    }

    public void UpdateColor()
    {
        rayColor = GetRandomColor();
        Laser.UpdateColor(rayColor);
        SetupCollider();
    }

    private void SetupCollider()
    {
        foreach (var ring in existingRings)
        {
            if (ring.Color != rayColor) continue;
            ring.AddColider();
            break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }

    [UsedImplicitly]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}