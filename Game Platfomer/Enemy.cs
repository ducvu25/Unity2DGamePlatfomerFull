using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float speed;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool IsGroundCheck()
    {
        return base.IsGroundCheck();
    }

    public override bool IsWallCheck()
    {
        return base.IsWallCheck();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
