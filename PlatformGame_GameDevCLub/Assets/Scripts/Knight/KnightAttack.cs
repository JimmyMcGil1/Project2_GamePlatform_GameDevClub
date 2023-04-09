using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    Animator anim;
    BoxCollider2D box;
    [Header("Attack")]
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 colliderRange;
    [SerializeField] float range;
    [SerializeField] float attackTimmer;
    [SerializeField] float attackTimmerNo2;
    float attackCoolDown;

    [SerializeField] float attackTimmerNo3;
    Rigidbody2D rigit;
    GameObject swordEffect;
    Vector2 realRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackCoolDown = 0f;
        box = GetComponent<BoxCollider2D>();
        swordEffect = GameObject.FindGameObjectWithTag("SwordEffect");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (attackCoolDown > attackTimmer)
            {
                anim.SetTrigger("attack");
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackCoolDown < attackTimmerNo2 )
                {
                    anim.SetTrigger("attack2");
                    attackCoolDown = 0;
                }
             attackCoolDown = 0;
            }
        }
     
       
        attackCoolDown += Time.deltaTime;
        realRange = colliderRange;
        realRange.x *= Mathf.Sign(transform.localScale.x);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere((Vector2)box.bounds.center + realRange, range);
    }
    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)box.bounds.center + realRange, range, enemyLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.CompareTag("Boss"))
            {
                hit[i].gameObject.GetComponent<Boss1_static>().TakeDame(-50);
                hit[i].gameObject.GetComponent<Animator>().SetTrigger("hurt");
            }
                
        }

    }
}
