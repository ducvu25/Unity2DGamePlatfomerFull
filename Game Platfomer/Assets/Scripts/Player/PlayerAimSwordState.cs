using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerSkillManager.instance.sword_Skill.SetDotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        PlayerSkillManager.instance.sword_Skill.SetDotsActive(false);
    }

    public override void Update()
    {
        base.Update();
        float fac = AimDirctetion().x;
        if ( (fac < 0 && player.facingRight) || (fac > 0 && !player.facingRight))
            player.FlipX();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            machine.ChangeState(player.idleState);
        }
        if(xInput != 0)
            player.SetVelocity(xInput, 0);
        
    }
    Vector2 AimDirctetion()
    {
        Vector2 playerP = player.transform.position;
        Vector2 mouseP = PlayerManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseP - playerP;
    }
}
