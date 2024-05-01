using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;
    private GameObject block;
    [SerializeField] public int blockCount;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Block")
        {
            AddBlock();
        }
    }

    private void AddBlock()
    {
        blockCount ++;
        GameObject newBlock = Instantiate(objectToInstantiate, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        newBlock.transform.position = new Vector3(newBlock.transform.position.x + 0.3f * blockCount, newBlock.transform.position.y , newBlock.transform.position.z);
    }
}
