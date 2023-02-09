using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] float maxSpeed = 1.0f;
    [SerializeField] [Range(1, 100)] float acceleration = 1.0f;
    [SerializeField] [Range(1, 100)] public float lookYSensitivity;
    [SerializeField] [Range(1, 100)] public float lookXSensitivity;
    [SerializeField] private Camera fpCamera;
    [SerializeField] private CharacterController controller;

    private float yLookAngle = 0;
    private Vector3 speed = Vector3.zero;
    private Vector3 moveBuffer;

    private float lookDeltaX = 0;
    private float lookDeltaY = 0;


    // Update is called once per frame
    void Update()
    {
        // rotate
        transform.Rotate(Vector3.up, lookDeltaX);
        yLookAngle = Mathf.Clamp(yLookAngle + lookDeltaY, -90, 90);
        fpCamera.transform.localRotation = Quaternion.AngleAxis(
            yLookAngle,
            Vector3.right
        );

        // move
        speed += Quaternion.LookRotation(transform.forward, Vector3.up) * moveBuffer;
        if (speed.magnitude > maxSpeed)
            speed = speed.normalized * maxSpeed;
        controller.SimpleMove(speed);


        lookDeltaX = 0;
        lookDeltaY = 0;
        //moveBuffer = Vector2.zero;
        speed *= .1f; 
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        Debug.Log($"MOVE {v}");
        moveBuffer = new Vector3(v.x, 0, v.y) * acceleration;
    }

    public void OnLook(InputValue value)
    {
        var lookDelta = value.Get<Vector2>();
        // Debug.Log($"LOOK {lookDelta}");
        if (lookDelta.magnitude < 0.1f) return;
        lookDeltaX = lookDelta.x * Mathf.Sqrt(lookXSensitivity) * .01f;
        lookDeltaY = -lookDelta.y * Mathf.Sqrt(lookYSensitivity) * .01f;
    }
}
