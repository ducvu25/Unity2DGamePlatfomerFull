using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected float distaceFindEnemy;
    protected float cooldownTimer = 0;
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        if(cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer > 0)
        {
            return false;
        }
        return true;
    }
    public virtual void UseSkill()
    {
        cooldownTimer = cooldown;
    }
    public Transform FindEnemyTransform(Transform t, float distance)
    {
        float distanceMin = -1;
        Transform enemyCheck = null;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(t.position, distance);
        //Debug.Log(collider2Ds.Length);

        foreach (var hit in collider2Ds)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead())
            {
                float d = Vector2.Distance(t.position, hit.transform.position);
                if (distanceMin == -1 || d < distanceMin)
                {
                    distanceMin = d;
                    enemyCheck = hit.transform;
                }
            }
        }
        //Debug.Log(enemyCheck.gameObject.name);
        return enemyCheck;
    }
    public Transform FindEnemyTransformRandom(Transform t, float distance)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(t.position, distance);
        List<Transform> list = new List<Transform>();

        foreach (var hit in collider2Ds)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead())
            {
                list.Add(hit.transform);
            }
        }
        if(list.Count > 0)
        {
            return list[Random.Range(0, list.Count - 1)];
        }
        return null;
    }
}
