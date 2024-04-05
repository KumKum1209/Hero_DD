using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBoss
{
    void OnEnter(BossController boss);
    void OnExecute(BossController boss);
    void OnExit(BossController boss);
}
