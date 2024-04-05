using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeroController : CharacterController
{
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] Renderer meshRenderer;
    [SerializeField] PantData pant;
    [SerializeField] Transform weaponPos;
    [SerializeField] Transform hatPos;
    [SerializeField] Transform botTarget;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float range;
    public AudioSource attackSound;
    private Vector3 joystickDirection;
    public WeaponData weapondata;
    public HatData hatdata;
    public ColorType color;
    public WeaponType weapon;
    public HatType hat;
    private float attackDelay = 2.0f;
    private Weapon currentWeapon;
    private Hat currentHat;
    public bool IsRunning;
    public GameManager gameManager;
    EnemyBaseController enemy;
    private float bulletSpeed = 10f;
    private float rotationSpeed = 30f;
    private float timer;


    public void ChangeWeapon(WeaponType weaponType)
    {
        //if (currentWeapon != null)
        //{
        //    Destroy(currentWeapon.gameObject);
        //    currentWeapon = Instantiate(weapondata.GetWeapon(weaponType), weaponPos);
        //}
        Instantiate(weapondata.GetWeapon(weaponType), weaponPos);
        currentWeapon = weapondata.GetWeapon(weaponType);
    }
    public void ChangeHat(HatType hatType)
    {
        //if (currentHat != null)
        //{
        //    Destroy(currentHat.gameObject);
        //    currentHat = Instantiate(hatdata.GetHat(hatType), hatPos);
        //}
        Instantiate(hatdata.GetHat(hatType), hatPos);
    }
    public void ChangePant(ColorType colorType)
    {
        color = colorType;
        meshRenderer.material = pant.GetMat(colorType);
    }

    void Start()
    {
        OnInit();
    }
    public override void OnInit()
    {

        base.OnInit();
        ChangePant(color);
        ChangeWeapon(weapon);
        ChangeHat(hat);
        //gameManager = FindObjectOfType<GameManager>();
        attackSound = GetComponent<AudioSource>();
        enemy = gameManager.GetEnemy();
        //botTarget = enemy.transform;
    }
    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
    }
    void Update()
    {
        if (gameManager.IsGame)
        {
            Vector3 moveDirection = new Vector3(joyStick.Horizontal, 0f, joyStick.Vertical).normalized;
            Vector3 moveVelocity = moveDirection * moveSpeed;

            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

            if (moveDirection != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
                ChangeAnim("run");
                IsRunning = true;
            }
            else
            {
                ChangeAnim("Idle");
                IsRunning = false;
            }

            if (OnTarget())
            {
                timer -= Time.deltaTime;
                if (!IsRunning && timer < 0f)
                {
                    if (attackSound != null)
                    {
                        attackSound.Play();
                    }
                    Attack();
                    ChangeAnim("attack");
                    timer = 1f;
                }

            }
        }

    }
    private void Attack()
    {
        EnemyBaseController closestEnemy = FindClosestEnemy();
        //Debug.Log(closestEnemy.name);
        if (closestEnemy != null)
        {
            GameObject bullet = Pool.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = shootPos.position;
                bullet.transform.rotation = shootPos.rotation;

                Vector3 directionToTarget = (closestEnemy.transform.position - bullet.transform.position).normalized;
                directionToTarget.y = 0f;

                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                transform.LookAt(transform.position + directionToTarget);
                bullet.transform.rotation = lookRotation;

                bullet.GetComponent<Rigidbody>().velocity = directionToTarget * bulletSpeed;
               

             
                
                


                bullet.SetActive(true);

                StartCoroutine(DeactivateBulletAfterTime(bullet));
            }
        }
    }

    private IEnumerator DeactivateBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(10f); 
        bullet.SetActive(false);
    }
    private bool OnTarget()
    {
        EnemyBaseController closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            return true;
        }
        return false;
    }

    private EnemyBaseController FindClosestEnemy()
    {
        EnemyBaseController[] enemies = GameObject.FindObjectsOfType<EnemyBaseController>();
        EnemyBaseController closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyBaseController enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < range && distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }
    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Instance.DeathHero(this);
    }

}
