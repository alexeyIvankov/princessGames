using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Transform panelTransform;
    public List<CharacterButton> buttons;
    public GameObject buttonInstance;
    

    private ShowRoom showRoom;
    // Start is called before the first frame update
    void Start()
    { 
       // Debug.Log("Selector Start");
        showRoom = FindObjectOfType<ShowRoom>();
        Debug.Log(showRoom.characterList.GetLength().ToString());
        for (int i = 0; i < showRoom.characterList.GetLength(); i++)
        {
            //Drawing buttons for all characters
            buttonInstance = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton"), panelTransform);
            buttons.Add(buttonInstance.GetComponent<CharacterButton>());
           // Debug.Log("Character id = " + showRoom.characterList.GetCharacter(i).GetId());
            buttons[i].SetupButton(showRoom.characterList.GetCharacter(i).GetId(), "Sprites/" + showRoom.characterList.GetCharacter(i).GetImageName());
        }
        
      buttons[showRoom.currentCharacter].SetSelected();
    }

    void Update()
    {
        //Select current character button
        buttons[showRoom.currentCharacter].SetSelected();
    }
    
}
