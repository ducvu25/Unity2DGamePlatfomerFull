using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerCatchSwordState : PlayerState
{
    GameObject goSword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        goSword = player.GetSword();
        if (goSword == null) return;
        if ((goSword.transform.position.x <player.transform.position.x && player.facingRight) || (goSword.transform.position.x > player.transform.position.x && !player.facingRight))
            player.FlipX();
        rb.velocity = (new Vector2(player.velocityReturnSword.x * (player.facingRight ? -1 : 1), player.velocityReturnSword.y));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
