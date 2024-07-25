
using UnityEngine;

public class Crystall_Skill_Controller : MonoBehaviour
{
    float timeAlive = 0;
    bool isAttack = false;

    float maxSize;
    float speed;
    Transform enemyTrans;
    float speedMove;
    Animator animator => GetComponent<Animator>();
    CircleCollider2D circleCollider => GetComponent<CircleCollider2D>();

    public void SetValue(float t, float size, float _speed, Transform enemyTras, float _speedMove)
    {
        timeAlive = t;
        maxSize = size;
        speed = _speed;
        enemyTrans = enemyTras;
        speedMove = _speedMove;
        isAttack = false;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyTrans == null)
        {
            if (!isAttack)
            {
                timeAlive -= Time.deltaTime;
                //Debug.Log(timeAlive);
                if (timeAlive < 0)
                {
                    isAttack = true;
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(maxSize, maxSize, maxSize), speed * Time.deltaTime);
            }
        }
        else
        {
            if (!isAttack)
            {
                //Debug.Log("ok");
                transform.position = Vector3.MoveTowards(transform.position, enemyTrans.position, speedMove * Time.deltaTime);
                if (Vector2.Distance(transform.position, enemyTrans.position) < 0.5f)
                {
                    isAttack = true;
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(maxSize, maxSize, maxSize), speed * Time.deltaTime);
            }
        }
        
    }
    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);
        foreach(var hit in collider2Ds)
        {
            EnemyStats enemyState = hit.GetComponent<EnemyStats>(); 
            if (enemyState != null)
            {
                PlayerManager.instance.player.stats.DoMagicalDamage(enemyState);// DoDamage(enemyState);
                hit.GetComponent<Enemy>().AddDame(transform.position);
            }
        }
    }

    public void Destroy() => Destroy(gameObject);
}
