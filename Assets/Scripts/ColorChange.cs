using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] public Renderer BrickMeshRenderer;
    [SerializeField] public Renderer PlayerMeshRenderer;

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Triggered");
        Debug.Log(BrickMeshRenderer.material.color);
        Debug.Log(BrickMeshRenderer.material.color != PlayerMeshRenderer.sharedMaterial.color);
        if(other.tag == "Player")
        {
            if(BrickMeshRenderer.material.color != PlayerMeshRenderer.sharedMaterial.color)
            {   
                BrickMeshRenderer.material.color = PlayerMeshRenderer.sharedMaterial.color;
            }
        }
        
    }
}
