using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TMP_Text text;
    [SerializeField] float timeGap = 0.01f;

    [SerializeField]  AudioSource audioSource;

    private void Update()
    {
        background.transform.forward = background.transform.position - Camera.main.transform.position;
    }

    public void SetText(string content)
    {
        StartCoroutine(IPlayText(content));
    }

    public void PlayAudio(AudioClip clip)
    {

        audioSource.Stop();

        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator IPlayText(string content)
    {
        text.text = content;
        text.maxVisibleCharacters = 0;
        while (text.maxVisibleCharacters <= content.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(timeGap);
        }
        yield return null;
    }

    public void Interact()
    {
        DialogueManager.Instance.InteractPanel();
    }
}
