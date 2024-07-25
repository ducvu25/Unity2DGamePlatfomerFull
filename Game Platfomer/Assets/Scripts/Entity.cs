using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("----------Infor----------")]
    public bool facingRight;

    [Header("\n----------Collider----------")]
    [Header("----------Ground----------")]
    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask groundCheckLm;
    [Header("----------Wall----------")]
    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask wallCheckLm;
    [Header("----------Attack----------")]
    public Transform attackCheck;
    public float attackCheckDistance;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public bool isHit;

    public System.Action actionFlip;
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    Color defaultColor;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();    
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        defaultColor = sr.color;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public void FlipX()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        if(actionFlip != null)
            actionFlip();
    }
    public void FlipController(float _x)
    {
        if ((_x < 0 && facingRight) || (_x > 0 && !facingRight))
            FlipX();
    }
    public void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
        FlipController(rb.velocity.x);
    }
    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        animator.speed = (1 -  _slowPercentage);    
    }
    protected virtual void ReturnDefaultSpeed()
    {
        animator.speed = 1;
    }
    #region Colision
    public virtual bool IsGroundCheck()
    {
        bool check = Physics2D.Raycast(groundCheck.position, Vector2.right * (facingRight ? 1 : -1), groundCheckDistance, groundCheckLm);
        return check;
    }
    public virtual bool IsWallCheck()
    {
        bool check = Physics2D.Raycast(wallCheck.position, Vector2.right * (facingRight ? 1 : -1), wallCheckDistance, wallCheckLm);
        return check;
    }
    public virtual Collider2D[] GetColliderAttack()
    {
        return Physics2D.OverlapCircleAll(attackCheck.position, attackCheckDistance);
    }
    public virtual void AddDame(Vector3 p)
    {
        //Debug.Log(value);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.right * (facingRight ? 1 : -1) * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (facingRight ? 1 : -1) * wallCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckDistance);
    }
    #endregion

    public virtual void Hint(bool value)
    {
        if (value)
        {
            sr.color = Color.clear;
        }
        else {
            sr.color = defaultColor;
        }
    }
}
