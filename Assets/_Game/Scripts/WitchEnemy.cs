using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WitchEnemy : EnemyBaseController
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;
  
    private Transform target;
    private float bulletSpeed = 10f;
    private float timer = 2f;
    private void Start()
    {
        OnInit();
        target = HeroTarget();
    }
    
    void Update()
    {
        if (OnRange(range) && IsAttack)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                //destination = Vector3.zero;
                Shoot();
                timer = 2f;
            }
            
                           
        }
        else
        {
            SetTarget();
            //ChangeState(new IdleState());
        }
            
    }
    private void Shoot()
    {
        ChangeAnim("Attack1");
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);


        Vector3 direction = (target.position - bullet.transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 500f * Time.deltaTime);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed ;
        Destroy(bullet.gameObject,2f);
    }

}
