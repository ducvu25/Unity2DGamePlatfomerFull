using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class PlayerController : Character
{
    
    [Header("\n-------Information------")]
    [SerializeField] float speed;
    [SerializeField] float jumpFoce;
    [SerializeField] int numberJump;
    int _numberJump;
    float moveX;
    
    

    [Header("\n-------Dash------")]
    [SerializeField] float speedDash;
    [SerializeField] float timeDash;
    [SerializeField] float delayDash;
    float _timeDash;
    float _delayDash;

    [Header("\n-------Attack------")]
    [SerializeField] float timeCombo;
    int typeAttack;
    float _timeCombo;
    bool isAttack;

    

    // Start is called before the first frame update
    protected override void Start()
    {
        _numberJump = 0;
        _timeDash = 0;
        _delayDash = 0;
        typeAttack = 0;
        _timeCombo = 0;
        isAttack = false;
        
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateTime();
        CheckInPut();
        Movement();
        UpdateAnimation();
    }
    void UpdateTime()
    {
        float delta = Time.deltaTime;
        if(_delayDash > 0)
        {
            _delayDash -= delta;
        }
        if(_timeCombo > 0)
        {
            _timeCombo -= delta;
        }
    }
    void CheckInPut()
    {
        if (_timeDash <= 0)
            moveX = Input.GetAxisRaw("Horizontal");
        if (isGround)
        {
            _numberJump = 0;
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }
    void Dash()
    {
        if(_delayDash <= 0)
        {
            _timeDash = timeDash;
            _delayDash = delayDash;
        }
    }
    public void AttackOver()
    {
        isAttack = false;
    }
    void Attack()
    {
        if(isAttack) { return; }
        isAttack = true;
        typeAttack = (typeAttack + 1) % 3;

        if (_timeCombo <= 0)
        {
            typeAttack = 0;
        }
        _timeCombo = timeCombo;
    }
    void Movement()
    {
        if(isAttack) { 
            //rb.velocity = Vector2.zero;
            return; 
        }
        if(_timeDash > 0)
        {
            _timeDash -= Time.deltaTime;
            rb.velocity = new Vector2(moveX*speedDash, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }
    }
    void Jump()
    {
        if (_numberJump < numberJump && !isAttack)
        {
            rb.velocity =  new Vector2(rb.velocity.x, jumpFoce);
            _numberJump++;
            //print(_numberJump);
        }
    }
    
    void UpdateAnimation()
    {
        if((rb.velocity.x < 0 && facingRight) || (rb.velocity.x > 0 && !facingRight))
            FlipX();

        StatePlayer state = StatePlayer.idle;
        if(Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            state = StatePlayer.run;
        }
        if(!isGround && _timeDash <= 0)
        {
            state = StatePlayer.jump;
            animator.SetFloat("velocityY", rb.velocity.y);
        }

        if (isAttack)
        {
            state = StatePlayer.attack;
            animator.SetInteger("typeAttack", typeAttack);
        }
        //print(state);
        animator.SetInteger("state", (int)state);
        if(!isAttack)
            animator.SetFloat("timeDash", _timeDash);
        
    }

    
}
