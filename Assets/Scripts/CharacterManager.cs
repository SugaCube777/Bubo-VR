using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<Character> characters = new List<Character>();

    public static CharacterManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public Character GetCharacter(string name)
    {
        return characters.Find(e => e.name == name || e.Name == name);
    }
}
