using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    public List<Character> characters = new List<Character>();
    
    // Start is called before the first frame update
    public CharacterList()
    {
        if (characters.Count != 0)
        {
            characters.Clear();
        }
        characters.Add(new Character("Elf", "Elf", "Геннадий"));
        characters.Add(new Character("Soldier", "Soldier", "Гоша"));
        characters.Add(new Character("Queen", "Queen", "Валентина"));
        characters.Add(new Character("Woman", "Woman", "Семён"));
    }

    public Character GetCharacter(int id)
    {
        return characters[id];
    }

    public int GetLength()
    {
        return characters.Count;
    }
}
