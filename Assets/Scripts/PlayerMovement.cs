using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public FloatingJoystick FloatingJoystick;
    public Rigidbody rb;
    public Animator anim;
    public string currentAnimName;

    public void FixedUpdate()
    {
        Debug.Log("Horizontal: " + FloatingJoystick.Horizontal + ", Vertical: " + FloatingJoystick.Vertical);
        Vector3 direction = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
        rb.velocity = direction * speed;
        if(FloatingJoystick.Horizontal != 0 || FloatingJoystick.Vertical !=0)
        {
            rb.velocity = direction * speed;
            rb.rotation = Quaternion.LookRotation(direction);

            ChangeAnim("running");
            Debug.Log("Changed");
        }
        else if(FloatingJoystick.Horizontal == 0 && FloatingJoystick.Vertical ==0)
        {
            ChangeAnim("idle");
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
    
}