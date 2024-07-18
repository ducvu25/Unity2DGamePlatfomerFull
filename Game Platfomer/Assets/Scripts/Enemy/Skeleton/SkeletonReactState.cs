using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonReactState : EnemyState
{
    Transform player;
    public SkeletonReactState(Skeleton skeleton, EnemyStateMachine stateMachine, string name) : base(skeleton, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        if((player.position.x < enemy.transform.position.x && enemy.facingRight) 
            || (player.position.x > enemy.transform.position.x && !enemy.facingRight)) {
            enemy.FlipX();
        }
        timeState = ((Skeleton)enemy).reactDelay;
        rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (timeState <= 0 && ((Skeleton)enemy).CanAttack())
        {
            stateMachine.SetState(((Skeleton)enemy).followPlayerState);
        }
    }
}
