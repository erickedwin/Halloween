using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIScaling : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Vector3 scalation;

    [SerializeField]
    private Vector3 secondScalation;

    [SerializeField]
    private float customTime = 0.4f;

    [SerializeField]
    Ease easiness = Ease.OutQuart;

    private Vector3 origin;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        origin = rectTransform.localScale;
    }

    public void ScaleOrigin()
    {
        rectTransform.DOScale(origin, customTime).SetUpdate(true);
    }

    public void ScaleBounceEase()
    {
        rectTransform.DOScale(origin, customTime).SetUpdate(true).SetEase(Ease.InBack);
    }

    public void ScaleOriginFancy()
    {
        rectTransform.DOScale(origin, customTime).SetUpdate(true).SetEase(easiness);
    }

    public void ScaleDestination()
    {
        rectTransform.DOScale(scalation, customTime).SetUpdate(true);
    }

    public void ScaleDestinationFancy()
    {
        rectTransform.DOScale(scalation, customTime).SetUpdate(true).SetEase(easiness);
    }
}
