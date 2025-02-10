using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timeState = ((Skeleton)enemy).randMoveState;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if ((!enemy.IsGroundCheck()|| enemy.IsWallCheck()) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            enemy.FlipX();
            stateMachine.SetState(((Skeleton)enemy).idleState);
            return;
        }

        if (timeState <= 0)
        {
            stateMachine.SetState(((Skeleton)enemy).idleState);
        }
        rb.velocity = new Vector2(enemy.speed*(enemy.facingRight ? 1 : -1), rb.velocity.y);
        
    }
}
