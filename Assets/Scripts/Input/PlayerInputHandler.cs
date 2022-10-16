using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions mainPlayerActions;

    private PlayerInput generalInput;

    private PlayerInputActions.PlayerActions player;
    private PlayerInputActions.UIActions uiControls;
    private PlayerInputActions.MenuManagersActions pause;

    private InputAction cameraMovement;

    public static PlayerInputHandler instance = null;

    private void Awake()
    {
        instance = this;
        mainPlayerActions = new PlayerInputActions();
        player = mainPlayerActions.Player;
        uiControls = mainPlayerActions.UI;
        pause = mainPlayerActions.MenuManagers;
        player.Enable();
        pause.Enable();
        generalInput = GetComponent<PlayerInput>();
        cameraMovement = generalInput.actions.FindAction(player.Look.name);
        cameraMovement.Enable();
    }

    public bool crouch => player.Crouch.triggered;

    public bool crouching => player.Crouch.IsPressed();

    public bool IsKeyboard => generalInput.currentControlScheme.Equals("Keyboard&Mouse", System.StringComparison.OrdinalIgnoreCase);

    public bool sprint => player.Sprint.triggered;

    public bool sprinting => player.Sprint.IsPressed();

    public Vector2 move => player.Move.ReadValue<Vector2>();

    public bool interact => player.Interact.triggered;

    public bool interacting => player.Interact.IsPressed();

    public bool jumping => player.Jump.triggered;

    public bool jumpingUp => player.Jump.WasReleasedThisFrame();

    //UI Controls
    public bool pauseMenu => pause.Pause.triggered;

    public bool nextPage => uiControls.NextPage.triggered;

    public bool previousPage => uiControls.PreviousPage.triggered;

    public void DisablePause()
    {
        pause.Disable();
    }

    public void EnablePause()
    {
        pause.Enable();
    }

    public void PauseInput()
    {
        player.Disable();

        cameraMovement.Disable();
        uiControls.Enable();
        UnlockCursor();
    }

    public void ResumeInput()
    {
        player.Enable();
        //uiControls.Disable();
        cameraMovement.Enable();
        uiControls.Disable();
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
