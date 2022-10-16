using DG.Tweening;
using Nocturne.GeneralTools;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingControl : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeTime;

    [SerializeField]
    private bool fadeAtStart = true;

    private void Start()
    {
        if (fadeImage == null) fadeAtStart = GetComponent<Image>();
        if (fadeAtStart)
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutSequence());
    }

    private IEnumerator FadeOutSequence()
    {
        fadeImage.DOFade(0f, fadeTime).SetUpdate(true);
        yield return Helpers.GetWaitRealtime(fadeTime);
        fadeImage.enabled = false;
    }

    public void FadeIn()
    {
        fadeImage.enabled = true;
        fadeImage.DOFade(1f, fadeTime).SetUpdate(true);
    }
}