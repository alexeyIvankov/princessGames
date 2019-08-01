using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour

{
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.instance;
    }
    
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Lelel_main");
        
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }
}