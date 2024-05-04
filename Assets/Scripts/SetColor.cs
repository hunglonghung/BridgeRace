using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scriptable
{
    public class SetColor : Character
    {
        public Material material;
        public GameObject stageFinishCheckPoint;
        [SerializeField] public bool canSpawn = false;
        [SerializeField] public float RandomizeColorTimer = 0f;

        void LateUpdate() 
        {
            int randomTime = Random.Range(3,6);
            //if block is taken
            if(material.color == gameObject.GetComponent<Renderer>().material.color)
            {
                RandomizeColorTimer += Time.deltaTime;
                if(RandomizeColorTimer > randomTime )
                {
                    RandomizeColor();
                    RandomizeColorTimer = 0;
                }
            }
            
            if(stageFinishCheckPoint != null)
            {
                canSpawn = stageFinishCheckPoint.GetComponent<StageFinish>().IsPassed;
            }
            if(canSpawn == false)
            {
                ChangeColor(ColorType.None);
            }
            // else
            // {
            //     int randomNumber = Random.Range(0, 4);
            //     if(randomNumber == 1)
            //     {
            //         ChangeColor(ColorType.Orange);
            //     }
                
            // }
        }
    }
}
