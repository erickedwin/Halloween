using DG.Tweening;
using Nocturne.Enums;
using Nocturne.GeneralTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    [Tooltip("La camara del juego")]
    private Transform cameraReference;

    [SerializeField]
    private Transform characterTransform;

    [SerializeField]
    [Tooltip("Los layers con los cuales el jugador puede detectar una colision")]
    private LayerMask collision;

    [Space]
    [Header("Movimiento")]
    [SerializeField]
    [Tooltip("La velocidad cuando camina")]
    private float normalSpeed = 5f;

    [SerializeField]
    [Tooltip("La velocidad cuando corre")]
    private float sprintSpeed = 9f;

    [SerializeField]
    [Tooltip("La velocidad cuando esta agachado")]
    private float crouchSpeed = 2.5f;

    [SerializeField]
    [Tooltip("La suavidad a la cual puede acelerar o desacelerar el jugador. Ponlo en 0 si quieres que sea instantaneo")]
    [Range(0f, 1f)]
    private float smoothMovement = .02f;

    [SerializeField]
    [Tooltip("Determina si el jugador debe mantener pulsado o no el boton de sprint para correr. Por defecto, solo basta pulsarlo una vez.")]
    private bool holdToSprint = false;
    [Space]
    [Header("Stamina")]
    [SerializeField]
    private bool usesStamina = false;
    [SerializeField]
    private float maxStamina = 100f;
    private float stamina;
    [SerializeField]
    private float delayRegen = 1f;

    private float delay;
    [SerializeField]
    private float drain = 1f;

    [SerializeField]
    private float regenMoving = 0.5f;

    [SerializeField]
    private float regenStill = 0.8f;

    [SerializeField]
    private Image staminaBar;

    [SerializeField]
    TextMeshProUGUI textStamina;

    bool exausted;

    [Space]
    [Header("Salto")]
    [SerializeField]
    [Tooltip("La gravedad a la cual cae el jugador (debe ser negativo)")]
    private float gravity = -9.8f;

    [SerializeField]
    [Tooltip("La altura a la cual el jugador puede saltar")]
    private float maxJumpHeight = 1f;

    [Range(0, 0.99f)]
    [SerializeField]
    [Tooltip("Multiplicador de cuando se podra cortar el salto si se ha soltado el boton.")]
    private float jumpCutMultiplier;

    [SerializeField]
    [Tooltip("Tiempo de Coyote: da un margen de tiempo para que el jugador pueda saltar aun cuando se haya salido del piso.")]
    private float coyoteTime;

    [SerializeField]
    [Tooltip("Buffer de tiempo: 'Cachea' la ultima vez que el boton de salto fue presionado para que pueda realizar el salto una vez toque el suelo. Hace que andar saltando constantemente sea más comodo.")]
    private float jumpBufferTime;

    //Detecta si esta saltando o no.
    private bool isJumping;

    //Detecta si esta en el suelo o no.
    private bool isGrounded;

    //Detecta si esta corriendo o no.
    private bool isRunning = false;

    //Detecta la ultima vez que ha soltado el input.
    //TODO: hacer que el Input System lo detecte sino.
    private bool jumpInputReleased;

    //La ultima vez que estuvo en el suelo. Funciona para el 'tiempo de coyote'
    private float lastGroundedTime;

    //El margen de tiempo para el salto para que pueda saltar despues de haber presionado el boton de salto.
    private float lastJumpTime;

    [Space]
    [Header("Agachado")]
    [SerializeField]
    [Tooltip("La altura a la cual el jugador va a estar agachado.")]
    private float crouchHeight;

    private float heightPlayer;

    [SerializeField]
    private bool holdToCrouch = false;

    //Variables Generales.
    private CharacterController characterController;

    private Transform currentTransform;

    private Vector3 currentMovement;

    //Ignora esto, solo sirve para hacer posible el movimiento suave.
    private Vector2 refInput;

    //Detecta el input actual.
    private Vector2 currentInput;

    //Detecta el input per se
    private Vector2 input;

    private Vector3 lookDir;

    //Estado del jugador.
    private PlayerStatus playerStatus = PlayerStatus.Idle;

    private Vector3 gravityMovement;

    // Start is called before the first frame update
    private void Start()
    {
        if (cameraReference == null)
        {
            cameraReference = Helpers.camera.transform;
        }

        if (!characterTransform)
        {
            characterTransform = Helpers.player.transform;
        }

        characterController = GetComponent<CharacterController>();
        currentTransform = gameObject.transform;
        heightPlayer = characterController.height;

        stamina = maxStamina;
        delay = delayRegen;
    }

    // Update is called once per frame
    private void Update()
    {
        SetInput();
        MoveCharacter();
        GravityHandler();
        //Jump();
        UpdatePlayerStatus();
        CrouchManager();
        JumpHandler();
        JumpManagement();
        if (usesStamina)
        {
            StaminaHandler();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = characterController.isGrounded;
    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir += cameraReference.right.normalized * PlayerInputHandler.instance.cameraMove.x;
        targetDir.Normalize();
        //targetDir.x = 0;
        //targetDir.z = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = characterTransform.forward;
        }
        Quaternion quat = Quaternion.LookRotation(targetDir);
        characterTransform.rotation = quat;
    }

    private void SetInput()
    {
        input = PlayerInputHandler.instance.move;
    }

    #region Movimiento

    private void MoveCharacter()
    {
        if (IsCrouching() && IsSprinting())
        {
            Uncrouch(PlayerStatus.Sprinting);
        }

        currentInput = Vector2.SmoothDamp(currentInput, input, ref refInput, smoothMovement);
        float speed = CanSprint() ? sprintSpeed : normalSpeed;
        if (IsCrouching()) speed = crouchSpeed;
        lookDir.x = -cameraReference.right.z;
        lookDir.z = cameraReference.right.x;

        currentMovement = lookDir * currentInput.y + cameraReference.right * currentInput.x;
        currentMovement.y = gravityMovement.y;
        currentTransform.TransformDirection(currentMovement);
        characterController.Move(currentMovement * speed * Time.deltaTime);
    }

    private void AlternativeMoveCharacter()
    {
        currentInput = Vector2.SmoothDamp(currentInput, input, ref refInput, smoothMovement);
        currentMovement.x = currentInput.x;
        currentMovement.z = currentInput.y;
        currentTransform.TransformDirection(currentMovement);
        float speed = CanSprint() ? sprintSpeed : normalSpeed;
        if (IsCrouching()) speed = crouchSpeed;
        characterController.Move(currentMovement * speed * Time.deltaTime);
    }

    #endregion Movimiento

    #region Salto

    private void GravityHandler()
    {
        gravityMovement.y += (gravity * Time.deltaTime);
        if (isGrounded && gravityMovement.y < 0)
        {
            gravityMovement.y = -1f;
        }

        characterController.Move(gravityMovement * Time.deltaTime);
    }

    private void JumpPhysics()
    {
        //Basado en una de las formulas de caida libre: Velocidad Final al cuadrado = Velocidad Inicial al cuadrado +/- 2 * Gravedad * Altura.
        //Como es salto, es decir, subida, el signo es +.
        //Como Velocidad final es 0 (porque queremos llegar a la altura maxima), se descarta y quedaria asi:
        // 0 = Velocidad Inicial al cuadrado + 2 * Gravedad * Altura.
        //Y para haya la velocidad/ fuerza inicial, se hace la conversion:
        // Velocidad Inicial = Raiz cuadrada (2 * Gravedad * Altura)
        //NOTA: pongo -2 porque como gravedad es negativo, se quiere convertir a positivo aqui.
        gravityMovement.y = Mathf.Sqrt(maxJumpHeight * -2.0f * gravity);

        lastGroundedTime = 0;
        lastJumpTime = 0;
        isJumping = true;
        jumpInputReleased = false;
    }

    private void JumpHandler()
    {
        if (PlayerInputHandler.instance.jumping)
        {
            OnJump();
        }

        if (PlayerInputHandler.instance.jumpingUp && CanJumpCut())
        {
            OnJumpCut();
        }
    }

    private void JumpManagement()
    {
        if (isGrounded)
        {
            lastGroundedTime = coyoteTime;
            isJumping = false;
        }
        else
        {
            if (lastGroundedTime > 0)
                lastGroundedTime -= Time.deltaTime;
        }

        if (lastJumpTime > 0)
        {
            lastJumpTime -= Time.deltaTime;
        }

        if (CanJump() && lastJumpTime > 0)
        {
            JumpPhysics();
        }
    }

    private void OnJump()
    {
        lastJumpTime = jumpBufferTime;
    }

    private void OnJumpCut()
    {
        if (gravityMovement.y > 0 && isJumping)
        {
            characterController.Move(Vector3.down * gravityMovement.y * (1 - jumpCutMultiplier) * 50);
        }

        jumpInputReleased = true;
        lastJumpTime = 0;
    }

    private bool CanJump()
    {
        return !isJumping && lastGroundedTime > 0;
    }

    private bool CanJumpCut()
    {
        return isJumping && gravityMovement.y > 0;
    }

    #endregion Salto

    #region SprintAndStamina
    private bool CanSprint()
    {
        if (exausted)
        {
            isRunning = false;
            return isRunning;
        }

        if (holdToSprint)
        {
            return PlayerInputHandler.instance.sprinting && input.y > 0;
        }
        else
        {
            if (PlayerInputHandler.instance.sprint && input.y > 0)
            {
                isRunning = true;
            }
            if ((input.y <= 0 && isRunning))
            {
                isRunning = false;
            }

            return isRunning;
        }
    }

    private void StaminaHandler()
    {
        
        if (IsSprinting())
        {
            DrainStamina();
            delay = delayRegen;
        }
        else
        {
            if(delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                RecoverStamina();
            }
            
        }

        staminaBar.fillAmount = stamina / maxStamina;
        textStamina.text = stamina.ToString("0");
    }

    public void DrainStamina()
    {
        stamina = Mathf.Clamp(stamina - (drain * Time.deltaTime * 10f), 0, maxStamina);
        if (stamina <= 0 && !exausted)
        {
            exausted = true;
            delay *= 2f;
        }
    }

    public void RecoverStamina()
    {
        if(stamina < maxStamina)
        {
            float gain = input.magnitude <= 0 ? regenStill : regenMoving;
            stamina = Mathf.Clamp(stamina + gain * Time.deltaTime * 10f, 0, maxStamina);
        }
        else
        {
            if (exausted) exausted = false;
        }
        
    }

    #endregion SprintAndStamina

    #region Agachado

    private bool CanCrouch()
    {
        return (isGrounded || playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.Walking);
    }

    //Este metodo revisa si hay algun obstaculo que impide que pueda levantarse.
    //Puede usarse para otros elementos tambien.
    private bool UpperBlocked()
    {
        Vector3 buttom = currentTransform.position - (Vector3.up * ((crouchHeight / 2) - characterController.radius));
        return Physics.SphereCast(buttom, characterController.radius, Vector3.up, out _, heightPlayer - characterController.radius, collision);
    }

    private void CrouchManager()
    {
        if (PlayerInputHandler.instance.sprint && playerStatus == PlayerStatus.Crouching)
        {
            Uncrouch(PlayerStatus.Sprinting);
            return;
        }
        if (!holdToCrouch)
        {
            if (PlayerInputHandler.instance.crouch)
            {
                if (playerStatus != PlayerStatus.Crouching)
                {
                    Crouch(true);
                }
                else
                {
                    Uncrouch();
                }
            }
        }
        else
        {
            if (PlayerInputHandler.instance.crouching)
            {
                Crouch(true);
            }
            else
            {
                Uncrouch();
            }
        }
        
    }

    private void Crouch(bool changeState = true)
    {
        if (!CanCrouch())
        {
            return;
        }
        //characterController.height = crouchHeight;
        DOTween.To(() => characterController.height, x => characterController.height = x, crouchHeight, 0.1f);
        if (changeState)
        {
            ChangeStatus(PlayerStatus.Crouching);
        }
    }

    private void Uncrouch(PlayerStatus status = PlayerStatus.Walking)
    {
        if (UpperBlocked())
        {
            print("No puede salir del crouch");
            return;
        }
        //characterController.height = heightPlayer;
        DOTween.To(() => characterController.height, x => characterController.height = x, heightPlayer, 0.1f);
        ChangeStatus(status);
    }

    public bool IsCrouching()
    {
        return playerStatus == PlayerStatus.Crouching;
    }

    #endregion Agachado

    #region Estados

    public bool IsIdle()
    {
        return playerStatus == PlayerStatus.Idle && characterController.isGrounded && Mathf.Abs(PlayerInputHandler.instance.move.magnitude) <= 0;
    }

    public bool IsSprinting()
    {
        return playerStatus == PlayerStatus.Sprinting && characterController.isGrounded;
    }

    //El cambio generico (de momento) para cambiar de estado.
    private void ChangeStatus(PlayerStatus newStatus)
    {
        if (playerStatus == newStatus) return;
        playerStatus = newStatus;
        //TODO: enlazarlo con animaciones, si los tenemos...
    }

    public void UpdatePlayerStatus()
    {
        if (playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.Walking || IsSprinting())
        {
            if (PlayerInputHandler.instance.move.magnitude > 0.02f)
            {
                ChangeStatus(CanSprint() ? PlayerStatus.Sprinting : PlayerStatus.Walking);
            }
            else
            {
                ChangeStatus(PlayerStatus.Idle);
            }
        }
    }

    #endregion Estados

    public void ChangeCrouchMode(bool crouchHoldMode)
    {
        holdToCrouch = crouchHoldMode;
    }

    public void ChangeSprintMode(bool sprintHold)
    {
        holdToSprint = sprintHold;
    }

    private void OnEnable()
    {
        SettingsManager.OnCrouchTypeChanged += ChangeCrouchMode;
        SettingsManager.OnSprintTypeChanged += ChangeSprintMode;
    }

    private void OnDisable()
    {
        SettingsManager.OnCrouchTypeChanged -= ChangeCrouchMode;
        SettingsManager.OnSprintTypeChanged -= ChangeSprintMode;
    }
}