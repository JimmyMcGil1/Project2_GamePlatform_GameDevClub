using System.Collections;
using UnityEngine;

public class Boss2_attack : MonoBehaviour
{
    Transform knightPos;
    CapsuleCollider2D box;
    public static int no;
    Animator anim;
    float attackRange;
    public float attack1_timmer;
    float attack1_counter;
    public float skill_timmer;
    float skill_counter;
    public GameObject spell;
    GameObject cloneSpell;
    GameObject cloneSpell1;
    GameObject cloneSpell2;
    Boss2_behavior boss;
    Vector2 realRange;
    [SerializeField] float range;
    [SerializeField] Vector2 colliderRange;
    [SerializeField] LayerMask knightLayer;
    int attack_normal;
    int attack_fury;
    bool isAttackFury;
    AudioSource audi;
    [SerializeField] AudioClip attack1Clip;
    [SerializeField] AudioClip attack2Clip;
    [SerializeField] AudioClip speellClip;

    private void Awake()
    {
        no = 0;
        audi.GetComponent<AudioSource>();
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
        boss = GetComponent<Boss2_behavior>();
        anim = GetComponent<Animator>();
        attack1_counter = 0;
        skill_counter = 0;
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
                if (Boss2_attack.no == 3)
                {
                    audi.clip = attack1Clip;
                    audi.Play();
                    anim.SetTrigger("attack_no");
                    isAttackFury = true;
                    Boss2_attack.no = 0;
                }
                else
                {
                    isAttackFury = false;
                    Boss2_attack.no++;
                    audi.clip = attack2Clip;
                    audi.Play();
                    anim.SetTrigger("attack_normal");
                }
                attack1_counter = 0;
            }
        }
        else
        {
            if (Boss2_static.fury == true && skill_counter > skill_timmer)
            {
                anim.SetTrigger("cast_spell");
                audi.clip = speellClip;
                audi.Play();
                Vector2 initPost = knightPos.position;
                initPost.y += 2f;
                cloneSpell  = Instantiate(spell, initPost, Quaternion.identity);
                cloneSpell1 = Instantiate(spell, initPost + Vector2.right * 1.5f, Quaternion.identity);
                cloneSpell2 = Instantiate(spell, initPost + Vector2.left * 1.5f, Quaternion.identity);
                StartCoroutine(HoldDestroy(cloneSpell));
                StartCoroutine(HoldDestroy(cloneSpell1));
                StartCoroutine(HoldDestroy(cloneSpell2));
                skill_counter = 0;
            }
        }
        attack1_counter += Time.deltaTime;
        skill_counter += Time.deltaTime;
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
                if (isAttackFury)  hit[i].GetComponent<KnightStatic>().TakeDame(-attack_fury);
                    else hit[i].GetComponent<KnightStatic>().TakeDame(-attack_normal);
            }

        }
    }
    
}
