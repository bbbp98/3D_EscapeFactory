using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float moveSpeed = 10f; //걷는 속도
    public float laneDistance = 2.73f;
    public float smoothSpeed = 10f; //대각선 이동

    [Header("Jump")]
    public float jumpPower = 7f;
    public LayerMask groundLayerMask;
    private bool isJumping = false;

    [Header("Slide")]
    public float slideDuration = 1f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isSliding = false;


    private int currentLane = 2;
    public float targetX;
    public AnimationHandler animationHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        targetX = transform.position.x;
        animationHandler = GetComponent<AnimationHandler>();
        if(animationHandler == null)
        {
            Debug.LogError("animatiorHandler is null");
        }
        if (IsGrounded())
        {
            animationHandler.NotJumpAnimation();
        }
    }


    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, targetX, Time.deltaTime * smoothSpeed);
        transform.position = newPos;

        if (isJumping && IsGrounded())
        {
            isJumping = false;
            animationHandler.NotJumpAnimation();

        }
    }
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed && currentLane > 0)
        {
            currentLane--;
            targetX = (currentLane-2) * laneDistance;
        }
    }
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed && currentLane < 4)
        {
            currentLane++;
            targetX = (currentLane-2) * laneDistance;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded() && !isSliding)
        {
            isJumping = true;
            animationHandler.JumpAnimation();
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded() && !isSliding)
        {
            isSliding = true;
            animationHandler.SlidingAnimation(true);
        }else if(context.canceled && isSliding){
            animationHandler.SlidingAnimation(false);
            isSliding = false;
        }
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position+(transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(-transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(transform.right*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(-transform.right*0.2f)+(transform.up*0.01f),Vector3.down),
        };
        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
