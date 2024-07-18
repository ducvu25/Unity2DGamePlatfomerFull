using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    
    public SkeletonIdleState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector3.zero;
        timeState = Random.Range(((Skeleton)enemy).randIdleState/2, ((Skeleton)enemy).randIdleState / 2);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(timeState <= 0) {
            stateMachine.SetState(((Skeleton)enemy).moveState);
        }
    }
}
