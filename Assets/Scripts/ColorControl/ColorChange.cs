using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] public Renderer BrickMeshRenderer;
    [SerializeField] public Renderer PlayerMeshRenderer;
    [SerializeField] public Renderer EnemyMeshRenderer;

    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log("Triggered");
        // Debug.Log(BrickMeshRenderer.material.color);
        // Debug.Log(BrickMeshRenderer.material.color != PlayerMeshRenderer.sharedMaterial.color);
        if(other.tag == "Player")
        {
            if(BrickMeshRenderer.material.color != PlayerMeshRenderer.sharedMaterial.color)
            {   
                BrickMeshRenderer.material.color = PlayerMeshRenderer.sharedMaterial.color;
            }
        }
        if(other.tag == "Enemy")
        {
            
            EnemyMeshRenderer = other.gameObject.GetComponent<BrickControl>().meshRenderer;
            Debug.Log(EnemyMeshRenderer.sharedMaterial.color);
            if(BrickMeshRenderer.material.color != EnemyMeshRenderer.sharedMaterial.color)
            {   
                other.gameObject.GetComponent<BrickControl>().blockCount --;
                BrickMeshRenderer.material.color = EnemyMeshRenderer.sharedMaterial.color;
            }
            
        }
        
    }
}
