using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Character
{
    [Header("\n-------Information------")]
    [SerializeField] float speed;
    [SerializeField] float timeState;

    [Header("\n-------Wall------")]
    [SerializeField] Transform playerCheck;
    [SerializeField] float distacePlayerCheck;
    [SerializeField] float distacePlayerAttack;
    [SerializeField] LayerMask lmPlayer;

    RaycastHit2D isPlayerDetected;
    
    float _timeState;
    StatePlayer state;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetState();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        

        if (!isPlayerDetected)
        {
            _timeState -= Time.deltaTime;
            if (_timeState < 0)
            {
                SetState();
            }
            if(state == StatePlayer.run)
            {
                Run();
            }
        }
        else
        {
            // lay thong tin may va cham
            // isPlayerDetected.collider.GetCompoment<PlayerController>()
            if(isPlayerDetected.distance > distacePlayerAttack) {
                rb.velocity = new Vector2((facingRight ? 1 : -1) * speed*1.5f, 0);
                state = StatePlayer.run;
            }
            else
            {
                state = StatePlayer.attack;
            }
        }
        if (!isGround || isWall)
        {
            FlipX();
        }
    }
    void SetState()
    {
        _timeState = Random.Range(0, timeState);
        state = (StatePlayer)Random.Range(0, 2);
    }
    void Run()
    {
        rb.velocity = new Vector2((facingRight ? 1 : -1) * speed, 0);
    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(playerCheck.position, Vector2.right, distacePlayerCheck* (facingRight ? 1 : -1), lmPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + Vector3.right * distacePlayerCheck * (facingRight ? 1 : -1));
    }
}
