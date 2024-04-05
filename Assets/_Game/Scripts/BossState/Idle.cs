using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Idle : IStateBoss
{
    float timer;
    public void OnEnter(BossController boss)
    {
        timer = Random.Range(0.5f, 1f);
    }

    public void OnExecute(BossController boss)
    {
        timer -= Time.deltaTime;
        if (timer <= 0.1f)
        {
            boss.ChangeState(new Patrol());
        }
    }

    public void OnExit(BossController boss)
    {
  
    }

}
