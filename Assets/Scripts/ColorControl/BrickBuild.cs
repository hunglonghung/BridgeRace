using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class BrickBuild : MonoBehaviour
{
    [SerializeField] public GameObject PlayerLimitCollider;
    [SerializeField] public BrickControl BrickControl;
    [SerializeField] public FloatingJoystick joystick;
    
    private void OnCollisionEnter(Collision other) 
    {
        joystick = other.gameObject.GetComponent<PlayerMovement>().FloatingJoystick;
        if(other.gameObject.tag == "Player" )
        {
            BrickControl = other.gameObject.GetComponent<BrickControl>();
            if(BrickControl.blockCount > 0 || joystick.Vertical < 0)
            {
                PlayerLimitCollider.GetComponent<BoxCollider>().isTrigger = true;
                Debug.Log("isTriggered");
                Debug.Log(PlayerLimitCollider.GetComponent<BoxCollider>().isTrigger);
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            BrickControl = other.gameObject.GetComponent<BrickControl>();
            if(BrickControl.blockCount == 0 && joystick.Vertical > 0)
            {
                PlayerLimitCollider.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}
