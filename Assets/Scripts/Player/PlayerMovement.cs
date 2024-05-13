using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : BaseMovement
{   
    public FloatingJoystick FloatingJoystick;
    [SerializeField] float joystickY;
    void FixedUpdate()
    {
        joystickY = FloatingJoystick.Vertical;
        CheckState();
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;

        switch (movementState)
        {
            case MovementState.Win:
                Win();
                break;
            case MovementState.Flat:
                MoveOnFlatGround(MoveDirection);
                break;
            case MovementState.Slope:
                MoveOnSlope(MoveDirection);
                break;
            case MovementState.Idle:
                Idle();
                break;
        }
        SpeedControl();

    }
    //state checking
    void CheckState()
    {
        if(isWin) movementState = MovementState.Win;
        else if(FloatingJoystick.Vertical == 0 && FloatingJoystick.Horizontal == 0) movementState = MovementState.Idle;
        else if(OnSlope()) movementState = MovementState.Slope;
        else movementState = MovementState.Flat;
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
        movementState = MovementState.Idle;

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
    //Winning
    private void Win()
    {
        ChangeAnim("dancing");
        rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.tag);
        if(other.tag == "Finish")
        {
            isWin = true;
            movementState = MovementState.Win;
        }
    }

}
