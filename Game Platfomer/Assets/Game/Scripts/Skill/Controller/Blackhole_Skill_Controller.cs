using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    float maxSize;
    float speed = 0;
    [SerializeField] List<KeyCode> keysCode;
    [SerializeField] GameObject goTxtKey;

    [SerializeField] float delayHole;

    int numberAttackEnemy;
    float delayAttack;
    float _delayAttack = 0;
    float distanceAttack;

    bool canHole = true;
    bool shrinkTheHole = false;
    bool isAttack = false;
    List<Transform> enemies;
    List<GameObject> hotKeys;
    bool isHint;
    public void SetValue(float maxSize,  float speed, int numberAttackEnemy, float delayAttack, float distanceAttack)
    {
        this.maxSize = maxSize;
        this.speed = speed;
        this.numberAttackEnemy = numberAttackEnemy;
        this.delayAttack = delayAttack;
        this.distanceAttack = distanceAttack;
        isHint = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Transform>();
        hotKeys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shrinkTheHole)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-1, -1, -1), speed*Time.deltaTime);
            if (transform.localScale.x <= 0)
            {
               Destroy(gameObject);
            }
            return;
        }
        if (isAttack)
        {
            CanAttack();
        }
        else
        {
            if (!canHole)
            {
                delayHole -= Time.deltaTime;
                if(delayHole <= 0)
                {
                    isAttack = true;
                    if(enemies.Count > 0)
                        PlayerManager.instance.player.Hint(isHint);
                    DestroyHotKeys();
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxSize + 1, maxSize + 1, maxSize + 1), speed * Time.deltaTime);
                if (transform.localScale.x >= maxSize)
                {
                    canHole = false;
                }
            }
        }
    }
    void CanAttack()
    {
        if ((enemies.Count <= 0 || numberAttackEnemy < 0) && isHint)
        {
            ShrinkTheHole();
            Invoke("ShowPlayer", 0.7f);
            return;
        }
        if (numberAttackEnemy >= 0 && enemies.Count > 0)
        {
            _delayAttack -= Time.deltaTime;
            if (_delayAttack <= 0)
            {
                numberAttackEnemy--;
                int x = Random.Range(0, enemies.Count);
                int dir = Random.Range(0, 100);
                PlayerSkillManager.instance.clone_Skill.InitCloneIdle(enemies[x], 0.2f, (dir >= 50 ? Vector3.right : Vector3.left) * distanceAttack, (dir >= 50 ? false : true));
                _delayAttack = delayAttack;
            }
        }
    }
    void ShowPlayer()
    {
        isHint = false;
        PlayerManager.instance.player.stateMachine.ChangeState(PlayerManager.instance.player.idleState);
        PlayerManager.instance.player.Hint(isHint);
    }
    void ShrinkTheHole()
    {
        shrinkTheHole = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && !collision.GetComponent<Enemy>().isDead())
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(false);
        }
    }
    void DestroyHotKeys()
    {
        for(int i=0; i<hotKeys.Count; i++)
        {
            Destroy(hotKeys[i]);
        }
        hotKeys.Clear();
    }
    private void CreateHotKey(Collider2D collision)
    {
        if (keysCode.Count == 0 || enemies.Count == numberAttackEnemy || isAttack || shrinkTheHole) return;
        GameObject go = Instantiate(goTxtKey, collision.transform.position + Vector3.up * 2, Quaternion.identity);
        hotKeys.Add(go);
        int x = Random.Range(0, keysCode.Count);
        go.GetComponent<Blackhole_HotKey_Controller>()?.SetValue(keysCode[x], collision.transform, this);
        keysCode.RemoveAt(x);
    }
    public void AddEnemyTransform(Transform e) => enemies.Add(e);
}
