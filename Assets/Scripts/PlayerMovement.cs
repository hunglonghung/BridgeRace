using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        Flat,
        Slope,
        Idle
    }
    [Header("Movement")]
    public float MoveSpeed;
    public FloatingJoystick FloatingJoystick;
    public Rigidbody rb;
    public Animator anim;
    public string currentAnimName;
    public MovementState currentState = MovementState.Flat;
    public Vector3 MoveDirection;
    [Header("Slope Handling")]
    [SerializeField] public float maxSlopeAngle;
    [SerializeField] public float directionMagnitute ;
    private RaycastHit slopeHit;

    void FixedUpdate()
    {
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
        // Detect the type of movement based on the slope
        UpdateMovementState();

        switch (currentState)
        {
            case MovementState.Flat:
                MoveOnFlatGround(MoveDirection);
                break;
            case MovementState.Slope:
                MoveOnSlope(MoveDirection);
                break;
        }
    }
    void UpdateRotation(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f) // Check if there's significant movement
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.fixedDeltaTime * 10); // Adjust 10 to your liking for smoothness
        }
    }


    void UpdateMovementState()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Change the threshold angle to suit your needs for what counts as a slope
            if (hit.normal != Vector3.up)
                currentState = MovementState.Slope;
            else
                currentState = MovementState.Flat;
        }
    }

    void MoveOnFlatGround(Vector3 direction)
    {
        directionMagnitute = MoveDirection.magnitude;
        if (direction.magnitude >= 0.1f) // Ensure some minimum movement threshold
        {
            rb.velocity = direction * MoveSpeed;
            rb.rotation = Quaternion.LookRotation(direction);
            ChangeAnim("running");
        }
        else
        {
            rb.velocity = Vector3.zero;
            ChangeAnim("idle");
        }
    }

    void MoveOnSlope(Vector3 direction)
    {
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1 * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, slopeHit.normal).normalized;
    }
}
