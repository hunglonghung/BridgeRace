using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class BrickBuild : MonoBehaviour
{
    [SerializeField] public GameObject PlayerLimitCollider;
    [SerializeField] public BrickControl BrickControl;
    
    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Player" )
        {
            BrickControl = other.gameObject.GetComponent<BrickControl>();
            if(BrickControl.blockCount > 0)
            {
                GameObject blockToRemove = BrickControl.blockList[BrickControl.blockCount - 1];
                BrickControl.blockList.RemoveAt(BrickControl.blockCount - 1);
                BrickControl.blockCount--;
                PlayerLimitCollider.SetActive(false);
                Destroy(blockToRemove);
            }
        }
        

    }
}
