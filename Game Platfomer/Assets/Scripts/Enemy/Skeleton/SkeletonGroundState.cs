using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{

    public SkeletonGroundState(Skeleton enemy, EnemyStateMachine stateMachine, string name) : base(enemy, stateMachine, name)
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
        if (((Skeleton)enemy).stats.GetHp() <= 0) return;
        if (((Skeleton)enemy).IsFindPlayer())
        {
            stateMachine.SetState(((Skeleton)enemy).reactState);
        }
    }
}
