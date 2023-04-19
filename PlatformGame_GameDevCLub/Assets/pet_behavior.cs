using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_behavior : MonoBehaviour
{
     [SerializeField] GameObject boss;
    GameObject knight;
    [SerializeField] float speed;
    [SerializeField] float force;
    Vector2 veloc;
    Vector2 velocAttack;
    [SerializeField] float attackTimmer ;
    float attackCounter ;
    float attackTimmerDone ;
    bool isAttack;
    private void Awake()
    {
        knight = GameObject.FindGameObjectWithTag("Knight");
        boss = GameObject.FindGameObjectWithTag("Boss");
        veloc = Vector2.zero;
        velocAttack = Vector2.zero;
        attackCounter = 0;
        attackTimmer = 5f;
        attackTimmerDone = attackTimmer + 0.8f;
        isAttack = false;
    }
    private void Update()
    {
        if (!isAttack)
        {
            Vector2 newPos = boss.transform.position + Vector3.up * 3  ;
            transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 0.5f);
        }
        
        if (attackCounter > attackTimmer)
        {
            if (attackCounter > attackTimmerDone)
            {
                isAttack = false;
                attackCounter = 0;
                return;
            }
            isAttack = true;
            //Attack();
            Vector2 newPos = knight.transform.position;
            transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 0.2f);
            //attackCounter = 0;
        }
        attackCounter += Time.deltaTime;
        
    }
    public void Attack()
    {
        Vector2 newPos = knight.transform.position ;
        // transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 1);
            transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 0.2f);

        Debug.Log("pet attack");
        isAttack = !isAttack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            KnightStatic.instance.TakeDame(-10);
          // collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
