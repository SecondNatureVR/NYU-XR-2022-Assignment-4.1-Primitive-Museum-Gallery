using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] float speed = 2.0f;
    [SerializeField] [Range(1, 100)] public float lookYSensitivity;
    [SerializeField] [Range(1, 100)] public float lookXSensitivity;
    [SerializeField] private Camera fpCamera;
    [SerializeField] private CharacterController controller;

    private bool isJumpBuffered = false;
    private float yLookAngle = 0;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Vector3 moveBuffer;
    private float lookDeltaX = 0;
    private float lookDeltaY = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Adapted from https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    void Update()
    {
        transform.Rotate(Vector3.up, lookDeltaX);
        yLookAngle = Mathf.Clamp(yLookAngle + lookDeltaY, -90, 90);
        fpCamera.transform.localRotation = Quaternion.AngleAxis(
            yLookAngle,
            Vector3.right
        );
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        controller.Move(Quaternion.LookRotation(transform.forward, Vector3.up)
            * moveBuffer
            * Time.deltaTime 
            * speed
        );

        // Changes the height position of the player..
        if (isJumpBuffered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        lookDeltaX = 0f;
        lookDeltaY = 0f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        moveBuffer = new Vector3(v.x, 0, v.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var lookDelta = context.ReadValue<Vector2>();
        // Debug.Log($"LOOK {lookDelta}");
        if (lookDelta.magnitude < 0.1f) return;
        lookDeltaX = lookDelta.x * Mathf.Sqrt(lookXSensitivity) * .01f;
        lookDeltaY = -lookDelta.y * Mathf.Sqrt(lookYSensitivity) * .01f;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumpBuffered = context.ReadValueAsButton();
    }
    public void AddForce(Vector3 force)
    {
        playerVelocity += force;
    }
}
