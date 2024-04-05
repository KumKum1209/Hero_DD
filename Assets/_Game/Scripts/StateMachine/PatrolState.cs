using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(EnemyBaseController bot)
    {
        //bot.RandomMove();
    }

    public void OnExecute(EnemyBaseController bot)
    {
        if (bot.OnRange(bot.range))
        {
            bot.ChangeState(new AttackState());
        }
        else
        { 
            bot.ChangeState(new IdleState()); 
        }
    }

    public void OnExit(EnemyBaseController bot)
    {

    }
}
