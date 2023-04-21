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
    [SerializeField] Vector2 suckPos;
    [SerializeField] Vector2 suckRange;
    int attack_normal;
    int attack_fury;
    bool isAttackFury;
    

    CapsuleCollider2D cap;
    [SerializeField] float suctionForce;
    [SerializeField] int timeSuck;

    [SerializeField] float suckSkillTimmer;
    float suckSkillCounter;
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
        cap = gameObject.GetComponent<CapsuleCollider2D>();
        suckSkillCounter = Mathf.Infinity;
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

        if (suckSkillCounter > suckSkillTimmer)
        {
            StartCoroutine(StartSuck(timeSuck));
            anim.SetBool("suck", true);
            suckSkillCounter = 0;
        }
        suckSkillCounter += Time.deltaTime;

      
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

        Vector2 realSuckPos = suckPos;
        realSuckPos.x = Mathf.Sign(transform.localScale.x) < 0 ? realSuckPos.x * -1 : realSuckPos.x;
        Gizmos.DrawWireCube(cap.bounds.center + (Vector3)realSuckPos, suckRange);
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
    IEnumerator StartSuck(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                SuckSkill();
            }
            yield return new WaitForSeconds(0.1f);
        }
        anim.SetBool("suck", false);
    }
    void SuckSkill()
    {
        Vector2 direction = transform.position - knightPos.transform.position;
        float distance = direction.magnitude;

        if (distance < suckRange.x)
        {
            float force = suctionForce * (1 - distance / suckRange.x);
            knightPos.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force);
        }
    }
   
}
