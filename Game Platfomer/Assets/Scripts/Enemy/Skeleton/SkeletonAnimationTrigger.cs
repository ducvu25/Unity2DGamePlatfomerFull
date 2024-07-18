using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    Skeleton player => GetComponentInParent<Skeleton>();

    void AnimationTrigger() => player.AnimationTrigger();
    void CheckAttack()
    {
        if (player.isAttack)
        {
            Collider2D[] collider2Ds = player.GetColliderAttack();
            foreach (var hit in collider2Ds)
            {
                if (hit.GetComponent<Player>() != null)
                    hit.GetComponent<Player>().AddDame(transform.parent.position, player.enemyInfor.GetDamage());
            }
        }
    }
}
