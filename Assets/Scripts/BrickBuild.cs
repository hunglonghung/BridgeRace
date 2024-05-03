using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class BrickBuild : MonoBehaviour
{
    [SerializeField] public GameObject PlayerLimitCollider;
    
    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Player")
        {
            if(BrickControl.blockCount > 0)
            {
                
                BrickControl.blockCount --;
                PlayerLimitCollider.SetActive(false);
            }
        }
        

    }
}
