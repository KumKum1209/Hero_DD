using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Patrol : IStateBoss
{
    public void OnEnter(BossController boss)
    {

        boss.RandomMovement();

    }

    public void OnExecute(BossController boss)
    {
        if (boss.OnRange(boss.range))
        {
            boss.ChangeState(new Attack());
        }
        else
        {
            boss.ChangeState(new Patrol());
        }
    }

    public void OnExit(BossController boss)
    {

    }
}
