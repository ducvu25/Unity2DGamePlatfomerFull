using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("\n------------ Idle infor -----------")]
    public float randIdleState;

    [Header("\n------------ Move infor -----------")]
    public float randMoveState;

    [Header("\n------------ Ract infor -----------")]
    public float playerCheckDistanceRact;
    public float reactDelay;

    [Header("\n------------ Follow infor -----------")]
    public float followDelay;
    public float folowCheckDistacne;

    [Header("\n------------ Attack infor -----------")]
    public float attackCheckDistacne;
    public float delayAttack;
    [HideInInspector] public float delayAttackLast;


    public EnemyStateMachine stateMachine;
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonReactState reactState { get; private set; }
    public SkeletonFollowPlayerState followPlayerState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    public SkeletonHitState hitState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        enemyInfor = GetComponent<Skeleton_Stats>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stateMachine = new EnemyStateMachine();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle");
        moveState = new SkeletonMoveState(this, stateMachine, "Move");
        reactState = new SkeletonReactState(this, stateMachine, "React");
        followPlayerState = new SkeletonFollowPlayerState(this, stateMachine, "Move");
        attackState = new SkeletonAttackState(this, stateMachine, "Attack");
        hitState = new SkeletonHitState(this, stateMachine, "Hit");
        deadState = new SkeletonDeadState(this, stateMachine, "Dead");
        stateMachine.InitState(idleState);

        delayAttackLast = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateMachine.state.Update();
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck.position - Vector3.right * (facingRight ? 1 : -1) * playerCheckDistanceRact/3, wallCheck.position + Vector3.right * (facingRight ? 1 : -1) * playerCheckDistanceRact*2/3);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (facingRight ? 1 : -1) * attackCheckDistacne);
    }
    public void AnimationTrigger()
    {
        isAttack = false;
        isHit = false;
    }
    public override bool IsGroundCheck()
    {
        return base.IsGroundCheck();
    }

    public override bool IsWallCheck()
    {
        return base.IsWallCheck();
    }
    public bool IsAttackPlayer()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * (facingRight ? 1 : -1), attackCheckDistacne, playerCheckLm);
    }
    public bool IsFindPlayer()
    {
        return Physics2D.Raycast(wallCheck.position - Vector3.right * (facingRight ? 1 : -1) * playerCheckDistanceRact / 3, Vector2.right * (facingRight ? 1 : -1), playerCheckDistanceRact*2/3, playerCheckLm);
    }
    public bool CanAttack()
    {
        if(Time.time > delayAttackLast + delayAttack)
        {
            delayAttackLast = Time.time;
            return true;
        }
        return false;
    }
    public override void AddDame(Vector3 p, int _stats)
    {
        base.AddDame(p, _stats);
        if (enemyInfor.GetHp() <= 0) return;
        if ((p.x > transform.position.x && !facingRight) || (p.x < transform.position.x && facingRight))
            FlipX();
        stateMachine.SetState(hitState);
        rb.velocity = new Vector2(1.5f*(facingRight ? -1 : 1), 4.5f);
        isHit = true;
        enemyInfor.TakeDamage(_stats);
    }
    public override void Die()
    {
        base.Die();
        stateMachine.SetState(deadState);
    }
}
