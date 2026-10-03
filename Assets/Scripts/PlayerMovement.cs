using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement & collision
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public Rigidbody2D rb2D;
    public BoxCollider2D box2D;

    // Interactions
    bool isInNPCRadius = false;

    // Anim
    bool isWalking = false;

    // State changes
    public bool motorSunk = false; // TEMP

    // Events

    void Awake()
    {
        motorSunk = false;
        GameStateManager.OnMotorSink += OnMotorSunk;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(PauseController.IsPaused)
        {
            rb2D.linearVelocity = Vector2.zero;
            isWalking = false; // later update for anim
            return;
        }
        rb2D.linearVelocity = moveInput * moveSpeed;
    }

    public void MoveHandler(InputAction.CallbackContext context)
    {
        if (motorSunk)
        {
            MoveInverted(context);
        }
        else
        {
            MoveNormal(context);
        }
    }

    void MoveNormal(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void MoveInverted(InputAction.CallbackContext context)
    {
        moveInput = - context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        // Check for overlap with object first

        if(context.performed && isInNPCRadius)
        {
            Debug.Log("Clicked E");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            isInNPCRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            isInNPCRadius = false;
        }
    }

    void OnMotorSunk()
    {
        motorSunk = true;
    }

    void OnDestroy()
    {
        GameStateManager.OnMotorSink -= OnMotorSunk;
    }

}
