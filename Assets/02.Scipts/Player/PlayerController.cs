using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float moveSpeed = 15f; //°È´Â ¼Óµµ
    public float laneDistance = 2.73f;

    [Header("Jump")]
    public float jumpPower = 20;
    public LayerMask groundLayerMask;
    private bool isJumping = false;
    public float customGravity = 20f;

    [Header("Slide")]
    public float slideDuration = 1f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isSliding = false;


    private int currentLane = 2;
    private Vector3 targetPosition;
    public AnimationHandler animationHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        targetPosition = transform.position;
        animationHandler = GetComponent<AnimationHandler>();
        if (animationHandler == null)
        {
            Debug.LogError("animatiorHandler is null");
        }
        if (IsGrounded())
        {
            animationHandler.NotJumpAnimation();
        }
    }
    void FixedUpdate()
    {
        float gravityMultiplier = IsGrounded() ? 1f : 2f;
        rb.AddForce(Vector3.down * customGravity * gravityMultiplier, ForceMode.Acceleration);

    }

    void Update()
    {
        if (GameManager.Instance.CurState != GameState.Playing)
        {
            animationHandler.anim.speed = 0f;
            return;
        }

        animationHandler.anim.speed = 1f;
        float nextZ = transform.position.z + moveSpeed * Time.deltaTime;

        Vector3 newPos = new Vector3(targetPosition.x, transform.position.y, nextZ);
        Vector3 direction = newPos - transform.position;
        float adjustedSpeed = moveSpeed;
        if (Mathf.Abs(direction.x) > 0.01f && Mathf.Abs(direction.z) > 0.01f)
        {
            adjustedSpeed *= 0.7f;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPos, adjustedSpeed * Time.deltaTime);
        if (isJumping && IsGrounded())
        {
            isJumping = false;
            //animationHandler.NotJumpAnimation();
        }
    }
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed && currentLane > 0)
        {
            currentLane--;
            targetPosition.x = (currentLane - 2) * laneDistance;
        }
    }
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed && currentLane < 4)
        {
            currentLane++;
            targetPosition.x = (currentLane - 2) * laneDistance;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isSliding)
        {
            isJumping = true;
            animationHandler.JumpAnimation();
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded() && !isSliding && !isJumping)
        {
            isSliding = true;
            animationHandler.SlidingAnimation(true);
        }
        else if (context.canceled && isSliding)
        {
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
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
    public void ChangeSpeedTemporaily(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }
    private IEnumerator ChangeSpeedCoroutine(float multiplier, float duration)
    {
        float originSpeed = moveSpeed;
        moveSpeed = originSpeed * multiplier;

        yield return new WaitForSeconds(duration);
        moveSpeed = originSpeed;
    }
}
