using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [Header("Referances")]
    private CharacterController controller;
    public InputActionReference interact;
    private bool hasRestoredSavedPosition;

    [Header("Movement")] [SerializeField] private float speed = 5;
    
    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundedGravity = -2f;

    private float verticalVelocity;
    
    [Header("Input")]
    private float moveInput;
    private float turnInput;
    
    private Interactor interaction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        interaction = GetComponent<Interactor>();

        if (controller == null)
        {
            Debug.LogError($"CharacterController is missing on {name}.", this);
            enabled = false;
        }
    }

    private void Start()
    {
        RestoreSavedPositionIfNeeded();
    }

    private void Update()
    {
        if (controller == null)
        {
            return;
        }

        InputManagement();
        Movement();
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
        move = transform.TransformDirection(move); // make movement relative to player

        move *= speed;

        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private void InputManagement()
    {
        moveInput = Input.GetAxis("Horizontal");
        turnInput = Input.GetAxis("Vertical");
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
