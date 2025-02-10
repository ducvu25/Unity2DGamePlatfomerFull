using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] float returnSwordSpeed;
    [SerializeField] float distancePlayerDestroy;
    [SerializeField] Vector2 vReturn;
    Player player;
    Rigidbody2D rb;
    Animator ani;
    BoxCollider2D box;
    bool canRotision = true;
    bool isReturnSword = false;

    SwordType type;

    [Header("-------- bouncing -------")]
    bool isBouncing = false;
    [SerializeField] float distaceEnemyBouncing;
    [SerializeField] int numberBouncing;
    List<GameObject> enemiesBouncing;
    int indexEnemiesBouncing;

    [Header("--------- Pierce -------- ")]
    [SerializeField] int numberbPierce;

    [Header("--------- Spine -------- ")]
    [SerializeField] float distaceEnemySpining;
    [SerializeField] float timeDelay;
    [SerializeField] float spineSpeed;
    [SerializeField] float spineSpeedTime;
    float timeLast = 0;
    float spineDir;

    float freezeTime;
    bool isTrigger = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        box = GetComponent<BoxCollider2D>();

        enemiesBouncing = new List<GameObject>();
    }
    void ReturnSword()
    {
        ani.SetBool("Return", true);
        isReturnSword = true;
        rb.isKinematic = false;
        box.isTrigger = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(canRotision)
        {
            transform.right = rb.velocity;
        }
        switch (type)
        {
            case SwordType.Bounce:
                {
                    Bouncing();
                    break;
                }
            case SwordType.Spin:
                {
                    Spine();
                    break;
                }
            case SwordType.Pierce:
                {
                    break;
                }
        }
        
        UpdateReturnSword();
    }
    bool checkRepeat = false;
    void Bouncing()
    {
        if (isBouncing)
        {
            if(enemiesBouncing.Count == 0)
            {
                isBouncing = false;
                ReturnSword();
                return;
            }
            float d = Vector2.Distance(transform.position, enemiesBouncing[indexEnemiesBouncing].transform.position);
            if (d > 0.2f)
                transform.position = Vector2.MoveTowards(transform.position, enemiesBouncing[indexEnemiesBouncing].transform.position, returnSwordSpeed * Time.deltaTime * 1.5f);
            else
            {
                EnemyStats enemyState = enemiesBouncing[indexEnemiesBouncing].transform.GetComponent<EnemyStats>();
                player.stats.DoDamage(enemyState);
                enemiesBouncing[indexEnemiesBouncing].transform.GetComponent<Enemy>().AddDame(player.transform.position);

                //enemiesBouncing[indexEnemiesBouncing].transform.GetComponent<Enemy>().AddDame(transform.position, player.stats.GetDamage());
                if(!checkRepeat)
                    StartCoroutine(enemiesBouncing[indexEnemiesBouncing].transform.GetComponent<Enemy>().FreezeTimeFor(freezeTime/3, 0.2f));
                indexEnemiesBouncing++;
                numberBouncing--;
                if (numberBouncing <= 0)
                {
                    isBouncing = false;
                    ReturnSword();
                }else if (indexEnemiesBouncing == enemiesBouncing.Count)
                {
                    indexEnemiesBouncing = 0;
                    checkRepeat = true;
                }
                
            }
        }
    }
    void Spine()
    {
        if (Time.time > timeLast + timeDelay)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, distaceEnemySpining);
            foreach (var hit in collider2Ds)
            {
                if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead())
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(spineDir * spineSpeed, 0, 0), spineSpeedTime*Time.deltaTime);
                    EnemyStats enemyState = hit.GetComponent<EnemyStats>();
                    player.stats.DoDamage(enemyState);
                    hit.GetComponent<Enemy>().AddDame(player.transform.position);
                }
            }
            timeLast = Time.time;
        }
    }
    void UpdateReturnSword()
    {
        if (isReturnSword)
        {
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (d > 0.2f)
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSwordSpeed * Time.deltaTime);
            else
            {
                player.stateMachine.ChangeState(player.catchSwordState);
                Destroy(gameObject);
            }
        }
        else
        {
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (d > distancePlayerDestroy)
                Destroy(gameObject);
        }
    }
    public void SetUpSword(Vector2 lauchDir, float gravity, Player player, SwordType swordType, float _freezeTime)
    {
        this.player = player;
        rb.velocity = lauchDir;
        rb.gravityScale = gravity;
        type = swordType;
        if(type == SwordType.Spin)
        {
            spineDir = Mathf.Clamp(rb.velocity.x, -1, 1);
        }
        freezeTime = _freezeTime;
    }
    void TriggerEnterBouncing(Collider2D collision)
    {
        if (!isBouncing)
        {
            if (collision.transform.GetComponent<Enemy>() != null)
            {
                checkRepeat = false;
                enemiesBouncing.Clear();
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, distaceEnemyBouncing);
                foreach (var hit in collider2Ds)
                {
                    if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead())
                    {
                        enemiesBouncing.Add(hit.transform.gameObject);
                    }
                }
                for (int i = 0; i < enemiesBouncing.Count; i++)
                    for (int j = i + 1; j < enemiesBouncing.Count; j++)
                    {
                        float d1 = Vector2.Distance(transform.position, enemiesBouncing[i].transform.position);
                        float d2 = Vector2.Distance(transform.position, enemiesBouncing[j].transform.position);
                        if (d1 > d2)
                        {
                            GameObject t = enemiesBouncing[i];
                            enemiesBouncing[i] = enemiesBouncing[j];
                            enemiesBouncing[j] = t;
                        }
                    }
                indexEnemiesBouncing = 0;
            }
            isBouncing = true;
        }
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            if (!isReturnSword && !isTrigger)
            {
                isTrigger = true;
                switch (type)
                {
                    case SwordType.Bounce:
                        {
                            TriggerEnterBouncing(collision);
                            break;
                        }
                    case SwordType.Spin:
                        {
                            rb.velocity = Vector2.zero;
                            rb.isKinematic = true;
                            rb.constraints = RigidbodyConstraints2D.FreezeAll;
                            break;
                        }
                    case SwordType.Pierce:
                        {
                            Enemy e = collision.GetComponent<Enemy>();
                            if (e != null && !e.isDead())
                            {
                                numberbPierce--;
                                if(numberbPierce == 0)
                                {
                                    SetRigidBody(collision);
                                    StartCoroutine(e.FreezeTimeFor(freezeTime, 0.2f));
                                }
                                EnemyStats enemyState = collision.GetComponent<EnemyStats>();
                                player.stats.DoDamage(enemyState);
                                collision.GetComponent<Enemy>().AddDame(player.transform.position);
                            }else
                                SetRigidBody(collision);
                            break;
                        }
                    case SwordType.Regular:
                        {
                            if (canRotision)
                            {
                                SetRigidBody(collision);
                                if(collision.GetComponent<Enemy>() != null && !collision.GetComponent<Enemy>().isDead())
                                {
                                    EnemyStats enemyState = collision.GetComponent<EnemyStats>();
                                    player.stats.DoDamage(enemyState);
                                    collision.GetComponent<Enemy>().AddDame(player.transform.position);
                                }
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (type)
                {
                    case SwordType.Regular:
                        {
                            Enemy e = collision.GetComponent<Enemy>();
                            if(e != null && !e.isDead())
                            {
                                StartCoroutine(e.FreezeTimeFor(freezeTime, 0.2f));
                                {
                                    EnemyStats enemyState = collision.GetComponent<EnemyStats>();
                                    player.stats.DoDamage(enemyState);
                                    collision.GetComponent<Enemy>().AddDame(player.transform.position);
                                }
                            }
                            
                            break;
                        }
                }
            }
            
        }
    }
    
    void SetRigidBody(Collider2D collision)
    {
        canRotision = false;
        ani.SetBool("Rotation", false);
        transform.parent = collision.transform;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnMouseDown()
    {
        if (!canRotision || type == SwordType.Spin)
        {
            ReturnSword();
        }
    }
}
