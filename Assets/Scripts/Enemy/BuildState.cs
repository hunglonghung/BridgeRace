using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState<EnemyCharacter>
{
    public void OnEnter(EnemyCharacter t)
    {
        t.MoveToFinishPoint();
    }

    public void OnExecute(EnemyCharacter t)
    {
        t.CheckBuildingState();
    }

    public void OnExit(EnemyCharacter t)
    {

    }

}
