using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    protected Enemy enemy;
    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
    }

    protected override void Die()
    {
        base.Die();
        
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
