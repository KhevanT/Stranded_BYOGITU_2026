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
    GameObject nearbyNPC;
    // public static event Action<NPC.NPC_ID> OnNPCInteracted;

    // Anim
    Animator animator;

    // State changes
    public bool motorSunk = false; 

    // Events

    void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        motorSunk = false;
        IslandSinkManager.OnMotorSink += OnMotorSunk;

        rb2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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
            animator.SetBool("isWalking", false);
            return;
        }

        if(moveInput == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
        }

        rb2D.linearVelocity = moveInput * moveSpeed;
    }

    public void MoveHandler(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
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
            // Debug.Log("Clicked E");
            nearbyNPC.GetComponent<NPC>().Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            isInNPCRadius = true;
            nearbyNPC = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC") && collision.gameObject == nearbyNPC)
        {
            isInNPCRadius = false;
            nearbyNPC = null;
        }
    }

    void OnMotorSunk()
    {
        motorSunk = true;
    }

    void OnDestroy()
    {
        IslandSinkManager.OnMotorSink -= OnMotorSunk;
    }

}
