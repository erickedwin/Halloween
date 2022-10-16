using DG.Tweening;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Vector3 destination;

    [SerializeField]
    private Vector3 secondDestination;

    [SerializeField]
    private float customTime = 0.4f;

    private Vector3 origin;

    [SerializeField]
    Ease easiness = Ease.OutQuart;

    //Enable this if you are going to use it for more varied UI elements.
    //public Ease easiness;
    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        origin = rectTransform.anchoredPosition3D;
    }

    public void MoveDestination()
    {
        rectTransform.DOAnchorPos3D(destination, customTime).SetUpdate(true).SetEase(Ease.OutBack);
    }

    public void MoveDestinationFancy()
    {
        rectTransform.DOAnchorPos3D(destination, customTime).SetUpdate(true).SetEase(easiness);
    }

    public void MoveSecondDestination()
    {
        rectTransform.DOAnchorPos3D(destination, customTime).SetUpdate(true);
    }

    public void MoveSecondDestinationFancy()
    {
        rectTransform.DOAnchorPos3D(secondDestination, customTime).SetUpdate(true).SetEase(easiness);
    }

    public void MoveOrigin()
    {
        rectTransform.DOAnchorPos3D(origin, customTime).SetUpdate(true).SetEase(Ease.InBack);
    }

    public void MoveOriginFancy()
    {
        rectTransform.DOAnchorPos3D(origin, customTime).SetUpdate(true).SetEase(easiness);
    }
}