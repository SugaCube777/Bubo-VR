using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueUtility : MonoBehaviour
{
    public List<Dialogue> Dialogues;
    public UnityEvent OnComplete = new UnityEvent();

    public void PlayDialogues()
    {
        DialogueManager.Instance.Play(this);
    }
}

[Serializable]
public class Dialogue
{
    public Transform Character;
    [TextArea] public string Body;
    public AudioClip AudioClip;
}
