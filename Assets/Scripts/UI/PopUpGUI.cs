using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//Esto hara posible que aparezca la interfaz adicional (el del codigo o cualquier otro que no sea el de pausa) para poder utilizarlo.
//Esto evitara que se ponga pausa cuando se interactua con el GUI.

public class PopUpGUI : MonoBehaviour
{
    public bool activated;

    public InputActionReference cancelButton;

    //public GameObject guiPanel;

    public UnityEvent OnShow;
    public UnityEvent OnHide;

    public bool exit => cancelButton.action.triggered;

    private void Start()
    {
    }

    private void Update()
    {
        if (PlayerInputHandler.instance.pauseMenu && activated)
        {
            print("Activado");
            Hide();
        }
    }

    public void SetPanel(GameObject panel)
    {
        //guiPanel = panel;
    }

    public void ActivatePopUp()
    {
        activated = true;
    }

    public void DeactivatePopUp()
    {
        activated = false;
    }

    public void Show()
    {
        activated = true;
        //guiPanel.SetActive(activated);
        OnShow.Invoke();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void Hide()
    {
        activated = false;
        //guiPanel.SetActive(activated);
        OnHide.Invoke();
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }
}