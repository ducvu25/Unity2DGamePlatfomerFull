using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    float vY = 0;
    float gravity;
    bool useSkill = true;
    
    public PlayerBlackHoleState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
    {
        vY = player.holeVelocityY;
    }

    public override void Enter()
    {
        base.Enter();
        useSkill = true;
        stateTimer = player.holeFlyTime;
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = gravity;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0)
        {
            rb.velocity = new Vector2(0, vY);
        }
        else
        {
            if(useSkill)
            {
                player.skillManager.hole_Skill.InitHole(player.transform.position);
                useSkill = false;
                rb.velocity = Vector2.zero;
            }
            else if(player.sr.color != Color.clear)
            {
                rb.velocity = new Vector2(0, -vY / 50);
            }
        }
    }
}
