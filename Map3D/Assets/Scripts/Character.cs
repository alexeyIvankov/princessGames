using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public String characterName;
    //Character Id Field
    //Needs to change 
    private static int _id;
    public int id;
    public String buttonSpriteName;

    public String prefabName;

    public Character(String prefabName, String buttonSpriteName, String name)
    {
        this.id = _id;
        _id++;
        //Gaystvo
        //Needs to change
        if (_id > 3)
        {
            _id = 0;
        }
        this.prefabName = prefabName;
        this.buttonSpriteName = buttonSpriteName;
        this.characterName = name;
    }

    public String GetImageName()
    {
        return buttonSpriteName;
    }

    public int GetId()
    {
        return id;
    }
}
