using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] DialoguePanel dialoguePanel;
    DialogueUtility dialogueUtility;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(DialogueUtility dialogueUtility)
    {
        this.dialogueUtility = dialogueUtility;
        StartCoroutine(IPlay());
    }

    bool pressed = false;
    IEnumerator IPlay()
    {
        dialoguePanel.gameObject.SetActive(true);
        for (int i = 0; i < dialogueUtility.Dialogues.Count; i++)
        {
            Dialogue dialogue = dialogueUtility.Dialogues[i];
            dialoguePanel.transform.position = dialogue.Character.position;
            dialoguePanel.SetText(dialogue.Body);
            dialoguePanel.PlayAudio(dialogue.AudioClip);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E) || pressed)
                {
                    pressed = false;
                    break;
                }    
            }
        }
        dialogueUtility.OnComplete.Invoke();
        dialoguePanel.gameObject.SetActive(false);
    }

    [ContextMenu("InteractPanel")]
    public void InteractPanel()
    {
        pressed = true;
    }
}
