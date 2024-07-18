using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonFollowPlayerState : EnemyState
{
    Transform player;
    public SkeletonFollowPlayerState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timeState = ((Skeleton)enemy).followDelay;
        player = PlayerManager.instance.player.transform;
        timeState = ((Skeleton)enemy).followDelay;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if ((!enemy.IsGroundCheck() || enemy.IsWallCheck()) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            enemy.FlipX();
            stateMachine.SetState(((Skeleton)enemy).idleState);
            return;
        }
        if (Vector2.Distance(enemy.transform.position, player.position) > ((Skeleton)enemy).folowCheckDistacne)
        {
            stateMachine.SetState(((Skeleton)enemy).idleState);
            return;
        }
        if (((Skeleton)enemy).IsAttackPlayer())
        {
            stateMachine.SetState(((Skeleton)enemy).attackState);
            return;
        }
        if (timeState <= 0)
        {
            stateMachine.SetState(((Skeleton)enemy).idleState);
        }
        if ((player.position.x < enemy.transform.position.x && enemy.facingRight)
            || (player.position.x > enemy.transform.position.x && !enemy.facingRight))
        {
            enemy.FlipX();
        }

        rb.velocity = new Vector2(enemy.speed* 1.7f * (enemy.facingRight ? 1 : -1), rb.velocity.y);

    }
}