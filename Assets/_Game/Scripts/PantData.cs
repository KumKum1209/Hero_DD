using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public enum ColorType
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Pink = 4,
        Grey = 5
    }
    [CreateAssetMenu(menuName = "PantData")]
    public class PantData : ScriptableObject
    {
        [SerializeField] Material[] materials;

        public Material GetMat(ColorType colorType)
        {
            return materials[(int)colorType];
        }
    }
}
