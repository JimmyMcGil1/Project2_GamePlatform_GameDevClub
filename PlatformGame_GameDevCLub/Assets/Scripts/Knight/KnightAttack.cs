using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] float fireBulltetTimmer;
    float fireBulletCounter;
    
    [SerializeField] AudioSource AttackSoundEffect;
    float attackCoolDown;
    Transform initBulletPos;
    [SerializeField] GameObject bullet;
    Rigidbody2D rigit;
    GameObject swordEffect;
    Vector2 realRange;
    [SerializeField] int flashDistance;
    [SerializeField] float flashTimmer;
     float flashCounter;
    int attackPower;
    KnightMoveset knigt_move;
    [Header ("Che")]
    [SerializeField] Image cheR;
    [SerializeField] Image cheM1; 


    private void Awake()
    {
        initBulletPos = gameObject.transform.Find("Init_Bullet_Pos").transform;
        //  bullet = gameObject.transform.Find("bullet").gameObject;
        knigt_move = GetComponent<KnightMoveset>();
        anim = GetComponent<Animator>();
        attackCoolDown = 0f;
        box = GetComponent<BoxCollider2D>();
        swordEffect = GameObject.FindGameObjectWithTag("SwordEffect");
        flashCounter = 0;
        fireBulletCounter = Mathf.Infinity;
    }
    private void Start()
    {
        attackPower = KnightStatic.instance.attackPower;
    }
    private void Update()
    {
        attackPower = KnightStatic.instance.attackPower;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
          //  AttackSoundEffect.Play();
            if (attackCoolDown > attackTimmer)
            {
                anim.SetTrigger("attack");
                AttackSoundEffect.Play();
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackCoolDown < attackTimmerNo2 )
                {
                    anim.SetTrigger("attack2");
                    attackCoolDown = 0;
                }
             attackCoolDown = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && (KnightStatic.instance.canFlash == 1))
        {
            if (flashCounter > flashTimmer) 
            {
                anim.SetTrigger("flash");
                flashCounter = 0;
                StartCoroutine(StartUnlock(flashTimmer, cheR));
            }
        }

        flashCounter += Time.deltaTime;
        attackCoolDown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && KnightStatic.instance.canShot == 1)
        {
            if (fireBulletCounter > fireBulltetTimmer)
            {
                anim.SetTrigger("shot_bullet");
           //     rigit.AddForce(Vector2.right * 10f * -Mathf.Sign(transform.localScale.x), ForceMode2D.Impulse);
                fireBulletCounter = 0;
                StartCoroutine(StartUnlock(fireBulltetTimmer, cheM1));
            }    
        }
        fireBulletCounter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && !knigt_move.IsGround())
        {
            anim.SetTrigger("attack_from_air");
        }
        realRange = colliderRange;
        realRange.x *= Mathf.Sign(transform.localScale.x);
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
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
                hit[i].gameObject.GetComponent<Boss1_static>().TakeDame(-attackPower);
                //hit[i].gameObject.GetComponent<Animator>().SetTrigger("hurt");
            }
            if (hit[i].gameObject.CompareTag("Boss2"))
            {
                hit[i].gameObject.GetComponent<Boss2_static>().TakeDame(-attackPower);
               // hit[i].gameObject.GetComponent<Animator>().SetTrigger("hurt");
            }
            if (hit[i].gameObject.CompareTag("Enemy"))
            {
                Debug.Log($"{attackPower}");
                hit[i].gameObject.GetComponent<enemy_static>().TakeDame(-attackPower);
                // hit[i].gameObject.GetComponent<Animator>().SetTrigger("hurt");
            }
            hit[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right  * Mathf.Sign(transform.localScale.x), ForceMode2D.Impulse);
        }

    }
    void Flash()
    {
        Vector2 newPos = transform.position;
        newPos.x = newPos.x + flashDistance * Mathf.Sign(transform.localScale.x);
        transform.position = newPos;
    }
    void FireBullet()
    {
        Instantiate(bullet, initBulletPos.position, Quaternion.identity);
    }
    IEnumerator StartUnlock(float second, Image che)
    {
        for (int i = 0; i < 20; i++)
        {
            che.fillAmount -= 0.05f;
            yield return new WaitForSeconds(second / 20);
        }
        che.fillAmount = 1;
    }
}
