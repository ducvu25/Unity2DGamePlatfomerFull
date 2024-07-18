using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
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
        if(xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed*0.8f, rb.velocity.y);
        if (player.IsWallCheck())
        {
            machine.ChangeState(player.wallState);
        }
        if (player.IsGroundCheck())
        {
            machine.ChangeState(player.idleState);
            rb.velocity = Vector2.zero;
        }
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && player.checkJumpCount())
        {
            machine.ChangeState(player.jumpState);
        }
    }
}
