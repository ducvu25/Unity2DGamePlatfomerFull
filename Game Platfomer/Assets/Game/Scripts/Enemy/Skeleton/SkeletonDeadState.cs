using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState :EnemyState
{
    public SkeletonDeadState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
