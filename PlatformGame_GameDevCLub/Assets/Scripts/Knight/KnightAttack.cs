using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    Animator anim;
    BoxCollider2D box;
    [Header("Attack")]
    [SerializeField] LayerMask itemLayer;
    [SerializeField] Vector2 colliderRange;
    [SerializeField] float range;
    [SerializeField] float attackTimmer;
    [SerializeField] float attackTimmerNo2;
    [SerializeField] AudioSource AttackSoundEffect;
    float attackCoolDown;
    GameObject swordEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackCoolDown = Mathf.Infinity;
        box = GetComponent<BoxCollider2D>();
        swordEffect = GameObject.FindGameObjectWithTag("SwordEffect");
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackSoundEffect.Play();
            if (attackCoolDown > attackTimmer)
            {
                anim.SetTrigger("attack");
                swordEffect.GetComponent<Animator>().SetTrigger("effect2");
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackCoolDown < attackTimmerNo2 && KnightMoveset.instance.IsGround())
                {
                    anim.SetTrigger("attack2");


                }
                attackCoolDown = 0;
            }
            //        if (hit.collider != null) hit.collider.GetComponent<Enemy>().TakeDame(20);
        }
        attackCoolDown += Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //        Gizmos.DrawWireSphere((Vector2)box.bounds.center + colliderRange * Mathf.Sign(transform.localScale.x), range);
    }
    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)box.bounds.center + colliderRange * Mathf.Sign(transform.localScale.x), range, itemLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            // hit[i].GetComponent<Enemy>().TakeDame(20);
        }

    }
}
