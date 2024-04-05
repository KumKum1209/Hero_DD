using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public enum HatType
    {
        Cowboy = 0,
        Crown = 1
    }
    [CreateAssetMenu(menuName = "HatData")]
    public class HatData : ScriptableObject
    {
        [SerializeField] Hat[] hats;

        public Hat GetHat(HatType hatType)
        {
            return hats[(int)hatType];
        }
    }
}
