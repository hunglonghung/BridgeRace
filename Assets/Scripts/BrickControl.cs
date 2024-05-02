using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptable;
using Unity.VisualScripting;
public class BrickControl : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;
    private GameObject block;
    [SerializeField] public int blockCount;
    [SerializeField] private GameObject objectToFollow;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Block" )
        {
            AddBlock();
            DeleteObject(other);
        }
    }

    private void DeleteObject(Collider block)
    {
        // block.gameObject.SetActive(false);
        Character characterComponent = block.GetComponent<Character>();
        if (characterComponent != null)
        {
            characterComponent.ChangeColor(ColorType.None);
        }
    }

    private void AddBlock()
    {
        blockCount ++;
        AddBlockOnPlayer();
    }
    private void AddBlockOnPlayer()
    {
        GameObject newBlock = Instantiate(objectToInstantiate, transform.position, Quaternion.identity,gameObject.transform);
        newBlock.transform.position = objectToFollow.transform.position + Vector3.up * (float)(1f + blockCount * 0.5f) + Vector3.back * 1f;
    }
}
