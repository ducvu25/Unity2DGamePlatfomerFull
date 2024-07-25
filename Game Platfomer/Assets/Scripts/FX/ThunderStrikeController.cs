using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikeController : MonoBehaviour
{
    [SerializeField] CharacterStats targetStats;
    [SerializeField] float speed;
    int _damage;
    Animator animator;
    bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void SetValue(int value, CharacterStats targetStats)
    {
        this._damage = value;
        this.targetStats = targetStats;
    }
    // Update is called once per frame
    void Update()
    {
        if (!targetStats) return;
        if (isTrigger) return;

        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed*Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;

        if(Vector2.Distance(transform.position, targetStats.transform.position) < 0.1f)
        {
            animator.transform.localPosition = new Vector3(-0.2f, 0, 0);
            transform.localRotation = Quaternion.identity;
            animator.transform.localRotation = Quaternion.identity; 
            transform.localScale = Vector3.one*2;

            isTrigger = true;
            targetStats.TakeDamage(_damage);
            animator.SetTrigger("Hit");
            Destroy(gameObject, 0.4f);
        }
    }
}
