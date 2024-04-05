using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponbullet;
    Transform targetHit;
    public float speed = 10f;
    private bool IsShoot;
    private GameObject bullet;
    private void Update()
    {
        if (IsShoot)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetHit.transform.position, speed * Time.deltaTime);

            if (bullet.transform.position == targetHit.transform.position)
            {
                IsShoot = false;
                Destroy(bullet.gameObject);
            }
        }
    }
    public void Spawn(Transform target)
    {
        bullet = Instantiate(weaponbullet, gameObject.transform);
        targetHit = target.transform;
        IsShoot = true;
    }

}
