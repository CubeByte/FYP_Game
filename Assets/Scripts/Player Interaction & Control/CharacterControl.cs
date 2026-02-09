using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [Header("Referances")]
    private CharacterController controller;
    public InputActionReference interact;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    
    [Header("Input")]
    private float moveInput;
    private float turnInput;
    
    private Interactor interaction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        interaction = GetComponent<Interactor>();
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        interaction.Interact();
    }
    
    private void Movement()
    {
        Vector3 move = new Vector3(moveInput, 0f, turnInput);

        move.y = 0;
        
        move *= speed;
        
        controller.Move(move * Time.deltaTime);
    }

    private void InputManagement()
    {
        moveInput = Input.GetAxis("Horizontal");
        turnInput = Input.GetAxis("Vertical");
    }

    private void OnEnable()
    {
        interact.action.started += Interact;
    }
    
    private void OnDisable()
    {
        interact.action.started -= Interact;
    }
}
