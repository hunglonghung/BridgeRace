using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        CheckState();
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;

        switch (currentState)
        {
            case MovementState.Flat:
                MoveOnFlatGround(MoveDirection);
                break;
            case MovementState.Slope:
                MoveOnSlope(MoveDirection);
                break;
            case MovementState.Idle:
                ChangeAnim("idle");
                break;
        }
        rb.useGravity = !OnSlope();
        SpeedControl();
    }
    void CheckState()
    {
        if(OnSlope()) currentState = MovementState.Slope;
        else if(FloatingJoystick.Vertical == 0 && FloatingJoystick.Horizontal == 0) currentState = MovementState.Idle;
        else currentState = MovementState.Flat;
    }


    //Flat ground movement
    void MoveOnFlatGround(Vector3 direction)
    {
        directionMagnitute = MoveDirection.magnitude;
        if (direction.magnitude >= 0.1f) // Ensure some minimum movement threshold
        {
            rb.velocity = direction * MoveSpeed;
            rb.rotation = Quaternion.LookRotation(direction);
            ChangeAnim("running");
        }
    }
    //Slope Movement

    void MoveOnSlope(Vector3 direction)
    {
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 500f, ForceMode.Force);
        }
        // Vector3 slopeDirection = GetSlopeMoveDirection();
        // float gravity = 9.81f;  
        // Vector3 gravityForce = Vector3.down * gravity * Time.fixedDeltaTime;
        // float slopeSpeedAdjustment = 1.5f; 
        // rb.velocity = (slopeDirection * MoveSpeed * slopeSpeedAdjustment) + gravityForce;
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1 * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.Log(angle < maxSlopeAngle && angle != 0);
            return angle < maxSlopeAngle && angle != 0;
            
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, slopeHit.normal).normalized;
    }
    //Animation
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    //Speed Control
    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope())
        {
            if (rb.velocity.magnitude > MoveSpeed)
                rb.velocity = rb.velocity.normalized * MoveSpeed;
        }
    }
}
