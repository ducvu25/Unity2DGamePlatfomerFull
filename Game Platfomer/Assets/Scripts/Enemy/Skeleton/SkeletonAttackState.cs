using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    public SkeletonAttackState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ((Skeleton)enemy).isAttack = true;
        rb.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!((Skeleton)enemy).isAttack)
        {
            stateMachine.SetState(((Skeleton)enemy).idleState);
        }
    }
}
