using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatePlayer
{
    idle,
    run,
    jump,
    attack
}
public class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool facingRight = true;

    [Header("\n-------Colider------")]
    [Header("\n-------Ground------")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float distaceGroundCheck;
    [SerializeField] protected LayerMask lmGround;

    [Header("\n-------Wall------")]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float distaceWallCheck;
    [SerializeField] protected LayerMask lmWall;

    protected bool isGround;
    protected bool isWall;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        facingRight = true;
        isGround = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }
    protected virtual void CollisionChecks()
    {
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, distaceGroundCheck, lmGround);
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right * (facingRight ? 1 : -1), distaceWallCheck, lmWall);
    }
    protected void FlipX()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        //GetComponentInChildren<SpriteRenderer>().flipX = !facingRight;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * distaceGroundCheck);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * distaceWallCheck * (facingRight ? 1 : -1));
    }
}
