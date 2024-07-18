using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    public Enemy enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    string name;

    protected Rigidbody2D rb;
    protected float timeState;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string name)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.name = name;
        rb = enemy.rb;
    }

    public virtual void Enter()
    {
        enemy.animator.SetBool(name, true);
    }
    public virtual void Update()
    {
        if (timeState > 0)
            timeState -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemy.animator.SetBool(name, false);
    }
}
