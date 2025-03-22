using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    [Header("¼àÌý")]
    public FadeScreenSO fadeEvent;

    public Image fadeImage;


    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;
    }
    private void OnFadeEvent(Color target, float duration,bool fadeIn)
    {
        fadeImage.DOBlendableColor(target, duration);
    }
}
