using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_attack : MonoBehaviour
{
    Transform knightPos;
    CapsuleCollider2D box;
    public static int no;
    Animator anim;
    float attackRange;
    public float attack1_timmer;
    float attack1_counter;
    public float skill_timmer;
    Boss3_behaviour boss;
    Vector2 realRange;
    [SerializeField] float range;
    [SerializeField] Vector2 colliderRange;
    [SerializeField] LayerMask knightLayer;
    int attack_normal;
    int attack_fury;
    bool isAttackFury;

    private void Awake()
    {
        no = 0;
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
        boss = GetComponent<Boss3_behaviour>();
        anim = GetComponent<Animator>();
        attack1_counter = Mathf.Infinity;
        attackRange = boss.attackRange;
        box = GetComponent<CapsuleCollider2D>();
        attack_normal = 50;
        attack_fury = 80;
        isAttackFury = false;
    }
    private void Update()
    {
        realRange = colliderRange;
        realRange.x *= Mathf.Sign(transform.localScale.x);

        if (Mathf.Abs(knightPos.position.x - boss.transform.position.x) <= attackRange)
        {

            if (attack1_counter > attack1_timmer)
            {
            
                
                    isAttackFury = false;
                    anim.SetTrigger("attack_normal");
                attack1_counter = 0;
            }
        }
      
        attack1_counter += Time.deltaTime;
    }
    IEnumerator HoldDestroy(GameObject _gameObject)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(2);
        }
        Destroy(_gameObject);
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere((Vector2)box.bounds.center + realRange, range);
    }
    public void Attack()
    {

        Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)box.bounds.center + realRange, range, knightLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.CompareTag("Knight"))
            {
                hit[i].gameObject.GetComponent<Animator>().SetTrigger("hit");
                 hit[i].GetComponent<KnightStatic>().TakeDame(-attack_normal);
            }

        }
    }
}
