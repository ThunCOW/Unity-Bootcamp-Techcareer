using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterYonetim : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    private float speed;

    public Camera cam;
    Rigidbody rb;
    Animator animator;
    int walk, run, jump;
    Vector3 verticalVector, horizontalVector, movementVector;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();

        walk = Animator.StringToHash("Walk");
        jump = Animator.StringToHash("Jump");
        run = Animator.StringToHash("Run");
    }

    void Update()
    {
        bool isWalking = animator.GetBool(walk);
        bool isRunning = animator.GetBool(run);
        bool isJumping = animator.GetBool(jump);
        bool isHoldingW = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S);
        bool isHoldingSpace = Input.GetKey(KeyCode.Space);
        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        if (!isWalking && isHoldingW)
        {
            animator.SetBool(walk, true);
            if (!isJumping)
                speed = WalkSpeed;
        }
        if (isWalking && !isHoldingW)
        {
            animator.SetBool(walk, false);
            animator.SetBool(run, false);
        }
        if (!isRunning && isHoldingShift)
        {
            animator.SetBool(run, true);
            if (!isJumping)
                speed = RunSpeed;
        }
        if (isRunning && !isHoldingShift)
        {
            animator.SetBool(run, false);
            if (!isJumping)
                speed = WalkSpeed;
        }
        if (!isJumping && isHoldingSpace)
        {
            animator.SetBool(jump, true);
        }
        if (isJumping && !isHoldingSpace)
        {
            animator.SetBool(jump, false);
        }

        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed;
        float verticalMovement = Input.GetAxisRaw("Vertical") * speed;

        verticalVector = cam.transform.forward * verticalMovement;
        horizontalVector = cam.transform.right * horizontalMovement;
            
        movementVector = verticalVector + horizontalVector;

        rb.velocity = new Vector3(movementVector.x, rb.velocity.y, movementVector.z);
        if (rb.velocity != Vector3.zero)
            transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
}
