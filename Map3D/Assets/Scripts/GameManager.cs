using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public GameObject enter;
    public bool visible;
    private static String sceneName = null;
    public bool endCol;

    private void Awake()
    {
        instance = this;
    }

    public void setNameScene(String sceneName1)
    {
        sceneName = sceneName1;
        Debug.Log("new scene " + sceneName);
    }

    public String getNameScene()
    {
        return sceneName;
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SuccessEnter()
    {
        String scene = sceneName;
        setNameScene(null);
        SceneManager.LoadScene(scene);
    }

    public void ExitHouse1()
    {
        //setNameScene(null);
        enter.SetActive(false);
        visible = false;
        //endCol = true;
    }

    public void EnterInHouse()
    {
        enter.SetActive(true);
    }
}