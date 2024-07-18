using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    SpriteRenderer sp;
    Animator ani;
    [SerializeField] float speedHint;
    [SerializeField] float timeIdle;
    [SerializeField] Transform attackCheck;
    [SerializeField] float attackCheckDistance;
    int isAttack = 0;
    Transform target = null;
    Vector3 indexPoint;
    bool facingRight;
    float distanceAttackMove = 1;
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        CheckEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float d = Vector2.Distance(transform.position, indexPoint);
            if (d >= distanceAttackMove)
                transform.position = Vector2.Lerp(transform.position, indexPoint, timeIdle * Time.deltaTime);
            if (isAttack == 0 && d < distanceAttackMove)
            {
                Attack();
            }
        }
        else
        {
            if (timeIdle > 0)
            {
                timeIdle -= Time.deltaTime;
            }
            else
            {
                if (isAttack == 0)
                    Attack();
            }
        }
        if (isAttack == 2)
        {
            sp.color = new Color(1, 1, 1, sp.color.a - Time.deltaTime * speedHint);
            if (sp.color.a <= 0)
                Destroy(gameObject);
        }
    }
    void Attack()
    {
        int type = Random.Range(1, 4);
        isAttack = 1;
        ani.SetInteger("Type", type);
    }
    void AnimationTrigger()
    {
        isAttack = 2;
        ani.SetInteger("Type", 0);
    }
    void CheckAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckDistance);
        foreach (var hit in collider2Ds)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead())
            {
                hit.GetComponent<Enemy>().AddDame(transform.position, PlayerManager.instance.player.playerInfor.GetDamage());
                return;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckDistance);
        Gizmos.DrawWireSphere(transform.position, attackCheckDistance * 3);
    }

    void CheckEnemy()
    {
        if (target != null && ((transform.position.x - 0.2f > target.position.x && facingRight) || (transform.position.x + 0.2f < target.position.x && !facingRight)))
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }
    public void Setvalue(float time, Vector3 index, bool face, float dis)
    {
        if (time > 0)
        {
            timeIdle = time;
        }
        indexPoint = index;
        facingRight = face;
        canMove = true;
        distanceAttackMove = dis;
    }
    public void Setvalue(float time, Transform enemy, bool face)
    {
        if (time > 0)
        {
            timeIdle = time;
        }
        target = enemy;
        facingRight = face;
        canMove = false;
    }
}
