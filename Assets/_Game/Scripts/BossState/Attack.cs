using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IStateBoss
{
    public void OnEnter(BossController boss)
    {
        boss.SetTarget();
        //boss.HandleComboAttack();
    }

    public void OnExecute(BossController boss)
    {
        
    }

    public void OnExit(BossController boss)
    {
        
    }
}
