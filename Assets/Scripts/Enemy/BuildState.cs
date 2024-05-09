using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState<EnemyCharacter>
{
    public void OnEnter(EnemyCharacter t)
    {
        t.SetBridgeTarget();
    }

    public void OnExecute(EnemyCharacter t)
    {
        t.BridgeBuild();
    }

    public void OnExit(EnemyCharacter t)
    {

    }

}
