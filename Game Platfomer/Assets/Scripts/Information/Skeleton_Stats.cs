using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Stats : EnemyStats
{
    public override void TakeDamage(int _stats)
    {
        base.TakeDamage(_stats);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Skeleton>();
    }

    protected override void Update()
    {
        base.Update();
    }
}
