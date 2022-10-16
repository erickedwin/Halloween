using DG.Tweening;
using TMPro;
using UnityEngine;

public class LoopingTextFade : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float loopingTime = 0.4f;

    private void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
        FadingLooping();
    }

    public void FadingLooping()
    {
        DOTween.To(() => text.alpha, x => text.alpha = x, 0.3f, loopingTime).SetLoops(-1,LoopType.Yoyo);
    }

    public void StopFadingLooping()
    {
        DOTween.Kill(text);
    }
}