using UnityEngine;

public class Player : Entity
{
    [Header("----------Move infor----------")]
    public float moveSpeed = 12f;

    [Header("----------Jump infor----------")]
    public float jumpFoce = 20f;
    [SerializeField] int jumpCount;
    int _jumpCount;

    [Header("----------Dash infor----------")]
    public float speedDash = 25f;
    public float dashDuration = 0.2f;

    [Header("----------Wall infor----------")]
    public float wallDelay = 0.2f;
    public Transform wallCheck2;
    public float wallCheckDistance2;

    [Header("----------Attack infor----------")]
    public Vector2[] speedAttack;
    public float attackDistanceClone;
    public float attackTimeClone;
    public float attackDistanceMove;

    [Header("\n----------Sword infor-----------")]
    GameObject goSword;
    public Vector2 velocityReturnSword;

    [Header("\n----------Hole infor-----------")]
    public float holeFlyTime;
    public float holeVelocityY;

    public PlayerStats stats;
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallState wallState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerBlackHoleState blackHoleState { get; private set; }    
    public PlayerDeahState deahState { get; private set; }
    public PlayerSkillManager skillManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stats = GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallState = new PlayerWallState(this, stateMachine, "Wall");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        hitState = new PlayerHitState(this, stateMachine, "Hit");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "ThrowSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Jump");
        deahState = new PlayerDeahState(this, stateMachine, "Deah");
        stateMachine.Initalize(idleState);
        facingRight = true;
        _jumpCount = 0;

        skillManager = PlayerSkillManager.instance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckInput();
    }
    void CheckInput()
    {
        if (Input.GetKey(KeyCode.A) && !isAttack && !IsWallCheck())
        {
            stateMachine.ChangeState(attackState);
        }
        if (Input.GetKeyDown(KeyCode.D) && stateMachine.currentState != blackHoleState && skillManager.crystal_Skill.CanUseSkill())
        {
            skillManager.crystal_Skill.UseSkill();  
        }
        if (Input.GetKeyDown(KeyCode.S) && skillManager.dash_Skill.CanUseSkill())
        {
            stateMachine.ChangeState(dashState);
        }
    }
    public void AnimationTrigger()
    {
        isAttack = false;
        isHit = false;
    }
    #region Colision
    public override bool IsGroundCheck() {
        bool check = base.IsGroundCheck();
        if(check)
            ResetJumpCount();
        return check;
    }
    public override bool IsWallCheck(){
        bool check = base.IsWallCheck();
        if (check)
            ResetJumpCount();
        return check;
    }
    public bool IsWallCheck2() => Physics2D.Raycast(wallCheck2.position, Vector2.right * (facingRight ? -1 : 1), wallCheckDistance2, wallCheckLm);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck2.position, wallCheck2.position + Vector3.right * wallCheckDistance2 * (facingRight ? -1 : 1));
    }
    #endregion
    void ResetJumpCount()
    {
        _jumpCount = 0;
    }
    public void JumpCount()
    {
        _jumpCount++;
    }
    public bool checkJumpCount()
    {
        if (_jumpCount < jumpCount)
        {
            return true;
        }
        return false;
    }
    public override void AddDame(Vector3 p)
    {
        base.AddDame(p);
        if ((p.x > transform.position.x && !facingRight) || (p.x < transform.position.x && facingRight))
            FlipX();
        stateMachine.ChangeState(hitState);
        rb.velocity = new Vector2(speedDash / 5 * (facingRight ? -1 : 1), jumpFoce / 5);
        isHit = true;
    }

    public GameObject GetSword()
    {
        return goSword;
    }
    public void SetSword(GameObject sword)
    {
        goSword = sword;
    }
}
