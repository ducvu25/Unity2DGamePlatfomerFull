using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float speed;
    float defaultSpeed;
    [SerializeField] protected LayerMask playerCheckLm;
    public EnemyStats stats;

    public ItemSO[] itemMission;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool IsGroundCheck()
    {
        return base.IsGroundCheck();
    }

    public override bool IsWallCheck()
    {
        return base.IsWallCheck();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    public virtual void FreezeTime(bool check)
    {
        if(check)
        {
            speed = 0;
            animator.speed = 0;
        }
        else
        {
            speed = defaultSpeed;
            animator.speed = 1;
        }
    }
    public bool isDead() => stats.GetHp() <= 0;
    public IEnumerator FreezeTimeFor(float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FreezeTimeFor(time));   
    }
    protected virtual IEnumerator FreezeTimeFor(float time)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(time);
        FreezeTime(false);
    } 
    public override void Die()
    {
        if (gameObject.layer == 10) return;
        base.Die();
        float x = Random.value * 100;
        List<int> indexSpawn = new List<int>();
        for (int j = 0; j < itemMission.Length; j++)
        {
            if (itemMission[j]._ratio > x)
            {
                indexSpawn.Add(j);
            }
        }
        if (indexSpawn.Count > 0)
        {
            GameManager.instance.SpawnItem(itemMission[indexSpawn[Random.Range(0, indexSpawn.Count-1)]], transform.position);
        }

        GameManager.instance.SpawnItem(transform.position);
        Destroy(gameObject, 3);
    }
    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        base.SlowEntityBy(_slowPercentage, _slowDuration);
        Debug.Log(speed);
        speed *= (1 -  _slowPercentage);
        Debug.Log(speed);
        Invoke("ReturnDefaultSpeed", _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        speed = defaultSpeed;
    }
}
