using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    private float forwardMoveSpeed = 1f;
    [SerializeField]
    private float backwardMoveSpeed = 1f;
    [SerializeField]
    private float jumpSpeed = 8f;
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private float airControlSensitivity = 1f;
    [SerializeField]
    private float sidestepSpeed = 1f;
    [SerializeField]
    private float gravity = 20.0f;
    [SerializeField]
    private Transform cameraTransform;

    // Private variables
    private bool jumping = false;
    private Vector3 moveVector = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalViewInput = Input.GetAxis("Mouse X");
        float verticalMoveInput = Input.GetAxis("Vertical");
        float horizontalMoveInput = Input.GetAxis("Horizontal");

        // Vector3 mvmtVector = new Vector3(hor, 0, ver);

        // TODO: Side stepping movement. 

        // Handle rotation
        transform.Rotate(Vector3.up, horizontalViewInput * turnSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {

            float speed = verticalMoveInput > 0 ? forwardMoveSpeed : backwardMoveSpeed;

            moveVector = transform.forward * verticalMoveInput + horizontalMoveInput * transform.right;
            moveVector = moveVector.normalized;
            moveVector *= speed;
            // moveVector += transform.right * sidestepSpeed * horizontalMoveInput;

            animator.SetFloat("ForwardSpeed", verticalMoveInput);
            animator.SetFloat("SidestepSpeed", horizontalMoveInput);
            bool sidestepping = (Mathf.Abs(verticalMoveInput) < 0.9) & (!Mathf.Approximately(horizontalMoveInput, 0));
            animator.SetBool("IsSidestepping", sidestepping);

            if (Input.GetButton("Jump"))
            {
                moveVector.y = jumpSpeed;
                animator.SetBool("IsJumping", true);
                // animator.SetFloat("Jump", -moveVector.y);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }
        else
        {
            // Now my feet don't touch the ground. 
            float y = moveVector.y;
            moveVector = airControlSensitivity * verticalMoveInput * transform.forward;
            moveVector += airControlSensitivity * horizontalMoveInput * transform.right;
            moveVector.y = y;
        }
        // Always apply gravity to ensure we stay grounded
        moveVector.y -= gravity * Time.deltaTime;
        // Debug.Log("move: " + moveVector);
        characterController.Move(moveVector * Time.deltaTime);

    }
}
