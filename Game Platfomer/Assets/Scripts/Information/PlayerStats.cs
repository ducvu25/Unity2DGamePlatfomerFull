using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;
    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
    }

    protected override void Die()
    {
        base.Die();
        player.stateMachine.ChangeState(player.deahState);
    }

    protected override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
    }

    protected override void Update()
    {
        base.Update();
    }
}
