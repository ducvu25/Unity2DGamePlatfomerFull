using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHitState : EnemyState
{
    public SkeletonHitState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!((Skeleton)enemy).isHit)
        {
            stateMachine.SetState(((Skeleton)enemy).idleState);
        }
    }
}
