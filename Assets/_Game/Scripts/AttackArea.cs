using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private EnemyBaseController enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        enemy.Attack();
        other.GetComponent<HeroController>().OnHit(30f);

    }
}
