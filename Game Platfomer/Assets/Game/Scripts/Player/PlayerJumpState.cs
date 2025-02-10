using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpCount();
        if(player.checkJumpCount())
            player.SetVelocity(rb.velocity.x, player.jumpFoce);
        else
            rb.velocity = new Vector2 (rb.velocity.x, player.jumpFoce*0.7f);
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
        if (rb.velocity.y <= -0.01f)
        {
            machine.ChangeState(player.airState);
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && player.checkJumpCount())
        {
            machine.ChangeState(player.jumpState);
        }
    }
}
