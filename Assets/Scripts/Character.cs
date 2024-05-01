using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public class Character : MonoBehaviour
    {
        [SerializeField] ColorData colorData;
        [SerializeField] Renderer meshRenderer;
        public ColorType Randomcolor;
        public ColorType color;

        public void ChangeColor(ColorType colorType)
        {
            color = colorType;
            meshRenderer.material = colorData.GetMat(colorType);
        }
        void Start()
        {
            RandomizeColor();
        }


        public void RandomizeColor()
        {

            int maxColorValue = System.Enum.GetValues(typeof(ColorType)).Length - 1; 
            ColorType randomColor = (ColorType)Random.Range(1, maxColorValue + 1); 
            ChangeColor(randomColor);
        }
    }
}
