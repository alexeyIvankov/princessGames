using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public String characterName;

    private static int Id;
    public int id;
    public String buttonSpriteName;

    public String prefabName;

    public Character(String prefabName, String buttonSpriteName, String name)
    {
        this.id = Id;
        Id++;
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
