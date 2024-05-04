using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public class SetColor : Character
    {
        public Material material;
        public GameObject stageFinishCheckPoint;
        [SerializeField] public bool canSpawn = false;

        void LateUpdate() 
        {
            int randomTime = Random.Range(2,10);
            // Debug.Log(gameObject.GetComponent<Renderer>().material);
            // Debug.Log(material);
            // Debug.Log(gameObject.GetComponent<Renderer>().material == material);
            if(material.color == gameObject.GetComponent<Renderer>().material.color)
            {
                Invoke("RandomizeColor",randomTime);
            }
            if(stageFinishCheckPoint != null)
            {
                canSpawn = stageFinishCheckPoint.GetComponent<StageFinish>().IsPassed;
            }
            // if(canSpawn == false)
            // {
            //     ChangeColor(ColorType.None);
            // }
            // // else
            // // {
            // //     int randomNumber = Random.Range(0, 4);
            // //     if(randomNumber == 1)
            // //     {
            // //         ChangeColor(ColorType.Orange);
            // //     }
                
            // // }
        }
    }
}
