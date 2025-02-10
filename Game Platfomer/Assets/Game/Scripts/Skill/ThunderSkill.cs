using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkill : Skill
{
    [SerializeField] GameObject goThuunder;
    PlayerStats player;

    protected override void Start()
    {
        player = PlayerManager.instance.player.stats;
    }
    public void InitThuner(Vector2 index)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(index, distaceFindEnemy);
        Transform e = null;
        //Debug.Log(collider2Ds.Length);
        float disMin = float.MaxValue;
        //int i = 0;
        foreach (var hit in collider2Ds)
        {
            float dis = Vector2.Distance(index, hit.transform.position);
            // Debug.Log(dis);
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead() && dis > 0.6f && dis < disMin)
            {
                //Debug.Log(i++);
                e = hit.transform;
                disMin = dis;
            }
        }

        if (e == null && GetComponent<Enemy>() != null && !GetComponent<Enemy>().isDead())
        {   
            e = transform;
        }
        if (e != null)
        {
            GameObject go = Instantiate(goThuunder, index, Quaternion.identity);
            go.transform.GetComponent<ThunderStrikeController>().SetValue(5, e.GetComponent<CharacterStats>());
        }
    }
}
