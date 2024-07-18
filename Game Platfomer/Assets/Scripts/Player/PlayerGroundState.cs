using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
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
        if(Input.GetKeyDown(KeyCode.R) && player.skillManager.hole_Skill.CanUseSkill())
        {
            machine.ChangeState(player.blackHoleState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.skillManager.sword_Skill.CanUseSkill() && player.GetSword() == null)
        {
            machine.ChangeState(player.aimSwordState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && player.IsGroundCheck() && !player.isAttack)
        {
            machine.ChangeState(player.jumpState);
            return;
        }
        if(rb.velocity.y < -0.4f)
        {
            machine.ChangeState(player.airState);
        }
    }
}