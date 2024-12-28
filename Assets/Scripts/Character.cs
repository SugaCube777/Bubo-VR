using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name = "Character";
    void Start()
    {
        CharacterManager.Instance.AddCharacter(this);
    }
}
