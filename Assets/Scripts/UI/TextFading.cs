using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFading : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float fadeTime;

    private void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
    }

    public void FadeIn()
    {
        DOTween.To(() => text.alpha, x => text.alpha = x, 1f, fadeTime);
    }

    public void FadeOut()
    {
        DOTween.To(() => text.alpha, x => text.alpha = x, 0f, fadeTime);
    }

    public void FadingLooping()
    {
        DOTween.To(() => text.alpha, x => text.alpha = x, 0.3f, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopFadingLooping()
    {
        DOTween.Kill(text);
    }
}