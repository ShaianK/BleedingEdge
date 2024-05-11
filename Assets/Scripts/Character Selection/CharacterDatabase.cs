using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public Character[] character;

    // Get the number of characters in the database
    public int CharacterCount
    {
        get { return character.Length; }
    }

    // Get a character from the database
    public Character GetCharacter(int index)
    {
        return character[index];
    }
}

