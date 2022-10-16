using DG.Tweening;
using Nocturne.GeneralTools;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float fadeTime;

    [SerializeField]
    private bool fadeAtStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null) fadeAtStart = GetComponent<CanvasGroup>();
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
        canvasGroup.DOFade(1f, fadeTime).SetUpdate(true);
        yield return Helpers.GetWaitRealtime(fadeTime);
        EnableCanvasGroup();
    }

    public void FadeIn()
    {
        DisableCanvasGroup();
        canvasGroup.DOFade(1f, fadeTime).SetUpdate(true);
    }

    public void EnableCanvasGroup()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void DisableCanvasGroup()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
