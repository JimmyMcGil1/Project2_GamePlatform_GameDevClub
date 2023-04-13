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
    

    private void Awake()
    {
        initBulletPos = gameObject.transform.Find("Init_Bullet_Pos").transform;
      //  bullet = gameObject.transform.Find("bullet").gameObject;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
          //  AttackSoundEffect.Play();
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
        if (Input.GetKeyDown(KeyCode.R) && (KnightStatic.instance.canFlash == 1))
        {
            if (flashCounter > flashTimmer) 
            {
                anim.SetTrigger("flash");
                flashCounter = 0;
            }
        }

        flashCounter += Time.deltaTime;
        attackCoolDown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (fireBulletCounter > fireBulltetTimmer)
            {
                anim.SetTrigger("attack");
                FireBullet();
                fireBulletCounter = 0;
            }    
        }
        fireBulletCounter += Time.deltaTime;
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
}
