using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{ 
    public enum WeaponType
    {
       Knife = 0,
       Hammer = 1
    }
    [CreateAssetMenu(menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] Weapon[] weapons;

        public Weapon GetWeapon(WeaponType weaponType)
        {
            return weapons[(int)weaponType];
        }
    }
}
