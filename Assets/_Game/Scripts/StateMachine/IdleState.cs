using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    public void OnEnter(EnemyBaseController bot)
    {
        //bot.ChangeAnim("idle");
        timer = Random.Range(0.5f, 1f);
    }

    public void OnExecute(EnemyBaseController bot)
    {
        timer -= Time.deltaTime;
        if (timer <= 0.1f)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(EnemyBaseController bot)
    {
       
    }

}
