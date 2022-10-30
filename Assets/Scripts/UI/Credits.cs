using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Sequence sequence;

    public float timeCredits;

    public RectTransform creditsObject;

    public float heightPoint;

    public TextMeshProUGUI textIndicator;

    public float maxTime = 4f;

    private float counterTime = 0;

    private bool activated = false;

    private float cooldown = 1f;

    private float cooldownPressApp = 0.2f;

    private PlayerInputActions shiningPlayerActions;

    //Esto hara que el jugador pueda salir de los creditos con cualquier boton o tecla.
    public bool activateform
    {
        get
        {
            return shiningPlayerActions.Player.Interact.triggered || shiningPlayerActions.Player.Fire.triggered || shiningPlayerActions.Player.Jump.triggered
                || shiningPlayerActions.MenuManagers.Pause.triggered || shiningPlayerActions.Player.Crouch.triggered || shiningPlayerActions.Player.Sprint.triggered
                || Keyboard.current.anyKey.wasPressedThisFrame;
        }
    }

    public bool changeScene
    {
        get
        {
            return shiningPlayerActions.Player.Fire.triggered || shiningPlayerActions.Player.Interact.triggered;
        }
    }

    private void Awake()
    {
        shiningPlayerActions = new PlayerInputActions();
        shiningPlayerActions.Player.Enable();
        shiningPlayerActions.UI.Enable();
        shiningPlayerActions.MenuManagers.Enable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        MovementCredits();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (activateform && cooldown <= 0 && !activated)
        {
            activated = true;
            counterTime = maxTime;
            cooldownPressApp = 0.1f;
            AppearText();
        }

        if (cooldownPressApp > 0 && activated)
        {
            cooldownPressApp -= Time.deltaTime;
        }

        if (counterTime > 0)
        {
            counterTime -= Time.deltaTime;
            if (changeScene && cooldownPressApp <= 0)
            {
                LoadScene();
            }
        }
        else
        {
            if (activated)
            {
                activated = false;
                DisappearText();
            }
        }
    }

    public void MovementCredits()
    {
        sequence = DOTween.Sequence();
        sequence.Append(creditsObject.DOAnchorPosY(heightPoint, timeCredits).SetEase(Ease.Linear));
    }

    public void AppearText()
    {
        DOTween.To(() => textIndicator.alpha, x => textIndicator.alpha = x, 1, 0.3f);
    }

    public void DisappearText()
    {
        DOTween.To(() => textIndicator.alpha, x => textIndicator.alpha = x, 0, 0.3f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}