using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
namespace Scriptable
{
    public class BrickControl : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] private GameObject objectToInstantiate;
    private GameObject block;
    [SerializeField] public int blockCount;
    [SerializeField] private GameObject objectParent;
    [SerializeField] Renderer meshRenderer;
    [SerializeField] public List<GameObject> blockList = new List<GameObject>();
    //trigger
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(meshRenderer.material.color);
        if(other.tag == "Block")
        {
            if(other.GetComponent<MeshRenderer>().material.color == meshRenderer.material.color)
            {
                SpawnBlock();
                DeleteObject(other);
            }
            
        }
    }
    //delete the brick collected
    private void DeleteObject(Collider block)
    {
        // block.gameObject.SetActive(false);
        Character characterComponent = block.GetComponent<Character>();
        if (characterComponent != null)
        {
            characterComponent.ChangeColor(ColorType.None);
        }
    }
    //Spawning
    private void SpawnBlock()
    {
        blockCount ++;
        SpawnBlockOnPlayer();
    }
    private void SpawnBlockOnPlayer()
    {
        Vector3 newPosition = new Vector3(0f, 0.58f * (blockCount-1), 0);
        Vector3 blockPosition = objectParent.transform.localPosition + newPosition;
        GameObject newBlock = Instantiate(objectToInstantiate, blockPosition, Quaternion.identity, objectParent.transform);
        newBlock.transform.localPosition = newPosition;
        newBlock.transform.localRotation = Quaternion.Euler(0,90,0);
        blockList.Add(newBlock);
        
        
    }
}

}
