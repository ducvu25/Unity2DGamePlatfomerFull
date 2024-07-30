using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] ItemSO item;
    [SerializeField] float speed;
    Transform target;

    SpriteRenderer sp;
    private void OnValidate()
    {
        sp = GetComponentInChildren<SpriteRenderer>();

        sp.sprite = item._img;
        gameObject.name = "Item: " + item._name;
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if(target != null)
        {
            float dis = Vector2.Distance(transform.position, target.position);
            if(dis > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
            }
            else
            {
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == null)
        {
            target = other.transform;
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

}
