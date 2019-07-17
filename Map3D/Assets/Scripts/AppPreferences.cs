using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppPreferences : MonoBehaviour
{

    public void SetCurrentCharacter(int id)
    {
    
        PlayerPrefs.SetInt("currentCharacter", id);
        PlayerPrefs.SetInt("isEverSaved", 28);
        PlayerPrefs.Save();
    }

    public int GetCurrentCharacter()
    {
        if (PlayerPrefs.HasKey("currentCharacter") && PlayerPrefs.HasKey("isEverSaved") && PlayerPrefs.GetInt("isEverSaved") == 28)
        {
     return PlayerPrefs.GetInt("currentCharacter");
            
        }
        else
        {
          return 0;
            
        }
    }

    private void OnDestroy()
    {
        
        PlayerPrefs.Save();
        
    }

    private void OnDisable()
    {
        
        PlayerPrefs.Save();
    }
    
}
