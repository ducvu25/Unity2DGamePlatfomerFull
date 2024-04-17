using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum StatePlayer
{
    idle,
    run,
    jump
}
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] float speed;
    [SerializeField] float jumpFoce;
    [SerializeField] float distaceCheckGround;

    int moveX;
    bool facingRight;
    bool isGround;
    [SerializeField] LayerMask lmGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, distaceCheckGround, lmGround);
        CheckInPut();
        UpdateAnimation();
    }
    void CheckInPut()
    {
        moveX = 0;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
            if(facingRight)
            {
                FlipX();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
            if (!facingRight)
            {
                FlipX();
            }
        }
        
        rb.velocity = new Vector2(moveX*speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpFoce));
    }
    void FlipX()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        //GetComponentInChildren<SpriteRenderer>().flipX = !facingRight;
    }
    void UpdateAnimation()
    {
        StatePlayer state = StatePlayer.idle;
        if(Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            state = StatePlayer.run;
        }
        if(!isGround)
        {
            state = StatePlayer.jump;
            animator.SetFloat("velocityY", rb.velocity.y);
        }
        animator.SetInteger("state", (int)state);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down*distaceCheckGround);
    }
}
