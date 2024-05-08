using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<EnemyCharacter>
{
    float timer;
    public void OnEnter(EnemyCharacter t)
    {
        t.ChangeAnim("idle");
        timer = 0;
    }

    public void OnExecute(EnemyCharacter t)
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            t.ChangeState(new CollectState());
        }
    }

    public void OnExit(EnemyCharacter t)
    {

    }

}
