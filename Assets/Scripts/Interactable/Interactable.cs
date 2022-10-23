using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent OnInteract;

    [SerializeField]
    [Range(0, 10f)]
    private float interactionRange = 3f;

    [SerializeField]
    private string message = "Interact";

    public bool canInteract = true;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public float GetRange() => interactionRange;

    public bool CanInteract() => canInteract;

    public string GetMessage() => message;
}