using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public class Character : MonoBehaviour
    {
        public enum ObjectChoice
        {
            Player,
            Enemy,
            Brick,
            Unbrick
        }
        [SerializeField] ColorData colorData;
        [SerializeField] Renderer meshRenderer;
        public ColorType Randomcolor;
        public ColorType color;
        [SerializeField ] public ObjectChoice ObjectType;
        

        public void ChangeColor(ColorType colorType)
        {
            color = colorType;
            meshRenderer.material = colorData.GetMat(colorType);
        }
        void Start()
        {
            if(ObjectType == ObjectChoice.Brick || ObjectType == ObjectChoice.Enemy)
            {
                RandomizeColor();
            }
            if(ObjectType == ObjectChoice.Player)
            {
                ChangeColor(ColorType.Orange);
            }
            if(ObjectType == ObjectChoice.Unbrick)
            {
                ChangeColor(ColorType.None);
            }
            
        }


        public void RandomizeColor()
        {
            int maxColorValue;
            if(ObjectType == ObjectChoice.Enemy)
            {
                maxColorValue = System.Enum.GetValues(typeof(ColorType)).Length - 2; 
            }
            else
            {
                maxColorValue = System.Enum.GetValues(typeof(ColorType)).Length - 1;
            }
            ColorType randomColor = (ColorType)Random.Range(1, maxColorValue + 1); 
            ChangeColor(randomColor);
        }
    }
}
