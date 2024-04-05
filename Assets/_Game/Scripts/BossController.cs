using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : EnemyBaseController
{
    private IStateBoss currentState;
    public enum BossPhase
    {
        Phase1,
        Phase2
    }
    

    public BossPhase currentPhase = BossPhase.Phase1;
    public float maxHealthPhase1;
    public float maxHealthPhase2 = 2000f;
    private int comboCount = 0;
    private float comboTimer = 0f;
    public float comboTimeLimit = 7f;

    private float currentHealth;
    private bool isMoving = false;
    private float moveTimer;
    private float moveInterval = 3f;

    void Start()
    {     
        OnInit();      
    }
    public override void OnInit()
    {
        base.OnInit();
        maxHealthPhase1 = hp;
        currentHealth = maxHealthPhase1;
    }
    void Update()
    {
       
        currentHealth = hp;
        
        switch (currentPhase)
        {
            case BossPhase.Phase1:
                if (currentHealth <= 500f)
                {
                    currentHealth = maxHealthPhase2; 
                    currentPhase = BossPhase.Phase2;
                   
                    ChangeAnim("Buff");
                    ChangeAnim("Run");
                }
                break;

        }

     
        switch (currentPhase)
        {
            case BossPhase.Phase1:
            
                if (!OnRange(range))
                {
                    ChangeState(new Idle());
                }
                else
                {
                    ChangeState(new Attack());
                }
                break;

            case BossPhase.Phase2:

                range = 30f;
                moveSpeed = 20f;
                if (OnRange(range))
                {
                    ChangeState(new Attack());
                }
                
                break;

        }
    }

  
    public void ChangeState(IStateBoss newState)
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
    public override void Attack()
    {

        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0)
            {
                comboCount = 0;
            }
        }

        if (comboCount == 0)
        {

           
            ChangeAnim("Attack1");
            comboCount++;
            comboTimer = comboTimeLimit;
        }
        else if (comboCount == 1)
        {
            ChangeAnim("Attack2");
            comboCount++;
            comboTimer = comboTimeLimit;
        }
        else if (comboCount == 2)
        {
         
            ChangeAnim("Attack3");
            comboCount = 0;
            comboTimer = 0f;
        }
        //}
    }
    public override void SetTarget()
    {

        if (OnRange(range))
        {
            Moving();
        }

        else
        {
            //ChangeAnim("Idle");
            ChangeState(new Idle());
        }

    }

    public void RandomMovement()
    {
        Debug.Log("random");
        if (!isMoving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveInterval)
            {
                StartMoving();
                moveTimer = 0f;
            }
        }
        else
        {

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StopMoving();
            }
        }
    }


    private void StartMoving()
    {
        ChangeAnim("Run");
        Vector3 randomPosition = GetRandomPositionOnNavMesh();
        agent.SetDestination(randomPosition);
        isMoving = true;
    }

    // Hàm dừng di chuyển
    private void StopMoving()
    {
        agent.isStopped = true;
        isMoving = false;
    }

    // Hàm lấy một điểm ngẫu nhiên trên NavMesh
    private Vector3 GetRandomPositionOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;
        bool found = false;

        while (!found)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);
            randomPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;
                found = true;
            }
        }

        return randomPosition;
    }
    
}
