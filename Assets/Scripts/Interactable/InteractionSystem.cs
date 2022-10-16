using Nocturne.GeneralTools;
using System;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField]
    private Transform currentCamera;

    [SerializeField]
    [Range(0, 10f)]
    private float interactionRange = 3f;

    [SerializeField]
    private LayerMask interactionLayer;

    private bool isInteracting;

    private IInteractable interactable;

    public static Action OnDetectInteractable;

    public static Action OnLeaveInteractable;

    // Start is called before the first frame update
    private void Start()
    {
        if (currentCamera == null)
        {
            currentCamera = Helpers.camera.transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        interactable = null;

        if (Physics.Raycast(currentCamera.position, currentCamera.forward, out var hitObject, interactionRange, interactionLayer))
        {
            float distance = Vector3.Distance(currentCamera.position, hitObject.point) + 0.05f;
            if (hitObject.collider.gameObject.TryGetComponent(out interactable))
            {
                if (distance > interactable.GetRange())
                {
                    interactable = null;
                }
                if (interactable != null && PlayerInputHandler.instance.interact)
                {
                    interactable.Interact();
                }
            }
        }

        DetectInteraction();
    }

    private void DetectInteraction()
    {
        if (interactable == null)
        {
            if (isInteracting)
            {
                isInteracting = false;
                OnLeaveInteractable?.Invoke();
            }
        }
        else
        {
            if (!isInteracting)
            {
                isInteracting = true;
                OnDetectInteractable?.Invoke();
                
            }
        }
    }
}