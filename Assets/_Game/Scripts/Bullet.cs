using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {     
        other.GetComponent<EnemyBaseController>().OnHit(50f);
        this.gameObject.SetActive(false);
    }
}
