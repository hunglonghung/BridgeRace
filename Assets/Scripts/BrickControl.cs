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
    [SerializeField] public static int blockCount;
    [SerializeField] private GameObject objectParent;
    [SerializeField] Renderer meshRenderer;

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

    private void DeleteObject(Collider block)
    {
        // block.gameObject.SetActive(false);
        Character characterComponent = block.GetComponent<Character>();
        if (characterComponent != null)
        {
            characterComponent.ChangeColor(ColorType.None);
        }
    }

    private void SpawnBlock()
    {
        blockCount ++;
        SpawnBlockOnPlayer();
    }
    private void SpawnBlockOnPlayer()
    {
        Vector3 relativePosition = new Vector3(0, 1f + blockCount * 0.5f, 0);
        Vector3 blockPosition = objectParent.transform.TransformPoint(relativePosition);
        Transform blockParent = objectParent.transform;
        GameObject newBlock = Instantiate(objectToInstantiate, blockPosition, Quaternion.identity, blockParent);
        newBlock.transform.position = new Vector3(transform.position.x, transform.position.y + 1f + blockCount * 0.5f, transform.position.z - 0.7f);
        newBlock.transform.rotation = Quaternion.Euler(0,90,90);
        
    }
}

}
