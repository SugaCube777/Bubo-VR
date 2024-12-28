using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIBlackscreen : MonoBehaviour
{
    public static UIBlackscreen Instance;
    Image image;

    private void Awake()
    {
        if (!Instance) Instance = this;
        image = GetComponent<Image>();
    }

    public void FadeIn(float duration)
    {
        image.DOFade(1, duration);
    }

    public void FadeOut(float duration)
    {
        image.DOFade(0, duration);
    }
}
