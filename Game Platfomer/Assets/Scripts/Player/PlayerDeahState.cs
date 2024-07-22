using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeahState : PlayerState
{
    public PlayerDeahState(Player player, PlayerStateMachine machine, string animationName) : base(player, machine, animationName)
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
        if(player.stats.GetHp() > 0)
        {
            machine.ChangeState(player.idleState);
        }
    }
}
