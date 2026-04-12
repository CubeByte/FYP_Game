using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [Header("Referances")]
    private CharacterController controller;
    public InputActionReference interact;
    private bool hasRestoredSavedPosition;
    public GameObject Light;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    
    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundedGravity = -2f;

    private float verticalVelocity;
    
    [Header("Input")]
    private float moveInput;
    private float turnInput;
    
    private Interactor interaction;

    [Header("Sprite Animation")]
    [SerializeField] private Material targetMaterial;
    [SerializeField] private string textureProperty = "_BaseMap";
    [SerializeField] private float animationSpeed = 0.2f;

    [Header("Idle Sprites")]
    [SerializeField] private Sprite idleSprite1;
    [SerializeField] private Sprite idleSprite2;

    [Header("Left Sprites")]
    [SerializeField] private Sprite leftSprite1;
    [SerializeField] private Sprite leftSprite2;

    [Header("Right Sprites")]
    [SerializeField] private Sprite rightSprite1;
    [SerializeField] private Sprite rightSprite2;

    private float animationTimer;
    private int animationFrame;

    private enum MovementState
    {
        Idle,
        Left,
        Right
    }

    private MovementState currentState = MovementState.Idle;
    private MovementState previousState = MovementState.Idle;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        interaction = GetComponent<Interactor>();

        if (controller == null)
        {
            Debug.LogError($"CharacterController is missing on {name}.", this);
            enabled = false;
            return;
        }

        if (targetMaterial == null)
        {
            Debug.LogWarning("Target Material is not assigned.", this);
        }
    }

    private void Start()
    {
        RestoreSavedPositionIfNeeded();
        UpdateAnimationState();
        ApplyCurrentSprite();
    }

    private void Update()
    {
        if (controller == null)
        {
            return;
        }

        InputManagement();
        Movement();
        UpdateAnimationState();
        UpdateSpriteAnimation();
    }

    private void RestoreSavedPositionIfNeeded()
    {
        if (hasRestoredSavedPosition || !ExplorationPlayerState.HasSavedTransform)
        {
            return;
        }

        controller.enabled = false;
        ExplorationPlayerState.Restore(transform);
        controller.enabled = true;
        hasRestoredSavedPosition = true;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        interaction.Interact();
    }
    
    private void Movement()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedGravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(moveInput, 0f, turnInput);
        move = transform.TransformDirection(move);

        move *= speed;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private void InputManagement()
    {
        moveInput = Input.GetAxis("Horizontal");
        turnInput = Input.GetAxis("Vertical");
    }

    private void UpdateAnimationState()
    {
        previousState = currentState;

        if (moveInput < -0.01f)
        {
            currentState = MovementState.Left;
            Light.SetActive(false);
        }
        else if (moveInput > 0.01f)
        {
            currentState = MovementState.Right;
            Light.SetActive(false);
        }
        else
        {
            currentState = MovementState.Idle;
            Light.SetActive(true);
        }

        if (currentState != previousState)
        {
            animationTimer = 0f;
            animationFrame = 0;
            ApplyCurrentSprite();
        }
    }

    private void UpdateSpriteAnimation()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            animationFrame = (animationFrame + 1) % 2;
            ApplyCurrentSprite();
        }
    }

    private void ApplyCurrentSprite()
    {
        if (targetMaterial == null)
        {
            return;
        }

        if (!targetMaterial.HasProperty(textureProperty))
        {
            Debug.LogWarning($"Material does not have texture property '{textureProperty}'.", this);
            return;
        }

        Sprite spriteToUse = GetSpriteForCurrentState();

        if (spriteToUse == null)
        {
            return;
        }

        targetMaterial.SetTexture(textureProperty, spriteToUse.texture);
    }

    private Sprite GetSpriteForCurrentState()
    {
        switch (currentState)
        {
            case MovementState.Left:
                return animationFrame == 0 ? leftSprite1 : leftSprite2;

            case MovementState.Right:
                return animationFrame == 0 ? rightSprite1 : rightSprite2;

            default:
                return animationFrame == 0 ? idleSprite1 : idleSprite2;
        }
    }

    private void OnEnable()
    {
        if (interact != null)
        {
            interact.action.started += Interact;
        }
    }
    
    private void OnDisable()
    {
        if (interact != null)
        {
            interact.action.started -= Interact;
        }
    }
}