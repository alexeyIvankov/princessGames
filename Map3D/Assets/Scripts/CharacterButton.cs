using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public RectTransform buttonTransform;
    public Image image;
    public int id;

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ClickHandler);
    }
    
    public void SetupButton(int id, String path, Boolean selected = false)
    {
        image.sprite = Resources.Load<Sprite>(path);
        this.id = id;
    }

    public void SetSelected()
    {
        button.Select();
    }
    void ClickHandler()
    {
        //Debug.Log("Button " + id + " Clicked" );
        button.Select();
        //Enable need_to_update_character flag
        FindObjectOfType<ShowRoom>().isUpToDate = false;
        //Change current character id
        FindObjectOfType<ShowRoom>().currentCharacter = id;
       
    }
    
}
