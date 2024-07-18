using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected float stateTimer;
    public Player player {  get; private set; }
    public PlayerStateMachine machine { get; private set; }

    protected float xInput;
    protected float yInput;
    private string animationName;

    public Rigidbody2D rb { get; private set; }

    public PlayerState(Player player, PlayerStateMachine machine, string animationName)
    {
        this.player = player;
        this.machine = machine;
        this.animationName = animationName;
        rb = player.rb;
    }

    public virtual void Enter()
    {
        //Debug.Log("I enter " + animationName);
        player.animator.SetBool(animationName, true);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        //Debug.Log("I in " + animationName);
        yInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }
    public virtual void Exit()
    {
        //Debug.Log("I exit " + animationName);
        player.animator.SetBool(animationName, false);
    }
}
