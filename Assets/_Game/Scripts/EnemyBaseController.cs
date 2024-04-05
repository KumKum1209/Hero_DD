using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBaseController : CharacterController
{
    private IState currentState;
    public NavMeshAgent agent;
    public Vector3 destination;
    public float range;
    //public GameManager gameManager;


    public HeroController hero;
    public bool IsAttack;
    public IState CurrentState { get => currentState; set => currentState = value; }
    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (hero != null )
        {
            SetTarget();
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        //gameManager = FindObjectOfType<GameManager>();
        hero = GameManager.instance.GetHero();
        IsAttack = GameManager.instance.IsGame;
    }
    public virtual void SetTarget()
    {

        if (OnRange(range) && IsAttack)
        {
            Moving();
        }

    }
    public void Moving()
    {
        ChangeAnim("Run");
        Vector3 destination = hero.transform.position;
        agent.destination = destination;
    }
    public void ChangeState(IState newState)
    {

        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public virtual void Attack()
    {
        ChangeAnim("Attack1");
    }
    public Transform HeroTarget()
    {
        return hero.transform;
    }
    public bool OnRange(float range)
    {
        float distanceToTarget = Vector3.Distance(transform.position, hero.transform.position);

        if (distanceToTarget < range)
        {
            return true;
        }
        else
            return false;


    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Instance.DeathEnemy(this);
    }
}