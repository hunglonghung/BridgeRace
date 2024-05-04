using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFinish : MonoBehaviour
{
    [SerializeField] public bool CharacterPassed = false; 
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] public bool IsPassed;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    // private void OnCollisionEnter(Collision other)
    // {
        
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         if(CharacterPassed == false)
    //         {   
    //             CharacterPassed = true; 
    //             boxCollider.isTrigger = true;
    //         }
            
    //     }
    // }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.isTrigger = false;
            IsPassed = true;
        }
        
    }


}

