using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Healthbar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] public float hp;
    public AudioSource m_AudioSource;

    public string animator { get => currentAnimName; set => currentAnimName = value; }
    private string currentAnimName;
    private bool IsDeath => hp < 0;
   


    void Update()
    {

    }
    public virtual void OnInit()
    {
        m_AudioSource = GetComponent<AudioSource>();
        healthBar.OnInit(hp, transform);
    }
    public virtual void OnHit(float damage)
    {
        Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        if (!IsDeath)
        {
            hp -= damage;
            m_AudioSource.Play();
            if (IsDeath)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHP(hp);
            
        }
    }
    public virtual void OnDeath()
    {
        ChangeAnim("Death");
        //Invoke(nameof(OnDespawn),4f);

    }
    //public void OnDespawn()
    //{
    //    ChangeAnim("Idle");
    //    OnInit();
    //}

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            
            anim.ResetTrigger(currentAnimName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }
}
