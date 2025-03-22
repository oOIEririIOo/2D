using UnityEngine;
using DG.Tweening;
using TMPro;

public class Test : MonoBehaviour
{
    public TextMeshPro tmp;

    private void OnEnable()
    {
        string text = tmp.text;
        var t = DOTween.To(() => string.Empty, value => tmp.text = value, text, 1f).SetEase(Ease.Linear);

        //¸»ÎÄ±¾
        t.SetOptions(true);
    }
}