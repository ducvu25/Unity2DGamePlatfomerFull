using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    void AnimationTrigger() => player.AnimationTrigger();
 
    void CheckAttack()
    {
        if (player.isAttack)
        {
            Collider2D[] collider2Ds = player.GetColliderAttack();
            foreach(var hit in collider2Ds)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    EnemyStats enemyState = hit.GetComponent<EnemyStats>();
                    player.stats.DoDamage(enemyState);
                    hit.GetComponent<Enemy>().AddDame(player.transform.position);
                }
                    //hit.GetComponent<Enemy>().AddDame(transform.parent.position, player.playerInfor.GetDamage());
            }
        }
    }
    void InitSword()
    {
        player.skillManager.sword_Skill.InitSword(transform.parent);
    }
    void IdleState()
    {
        player.stateMachine.ChangeState(player.idleState);
    }
}
