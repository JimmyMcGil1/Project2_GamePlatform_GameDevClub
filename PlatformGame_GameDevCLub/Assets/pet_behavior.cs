using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_behavior : MonoBehaviour
{
    GameObject boss;
    GameObject knight;
    [SerializeField] float speed;
    [SerializeField] float force;
    Rigidbody2D rigit;
    Vector2 veloc;
    float attackTimmer ;
    float attackCounter ;
    bool isAttack;
    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        knight = GameObject.FindGameObjectWithTag("Knight");
        Debug.Log(boss.name);
        rigit = GetComponent<Rigidbody2D>();
        veloc = rigit.velocity;
        attackCounter = 0;
        attackTimmer = 5f;
        isAttack = false;
    }
    private void Update()
    {
        if (!isAttack)
        {
            Vector2 newPos = boss.transform.position - transform.position + Vector3.up * 1.5f;
            transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 1);
        }
        
        if (attackCounter > attackTimmer)
        {
            isAttack = true;
            Attack();
            attackCounter = 0;
        }
        attackCounter += Time.deltaTime;
    }
    public void Attack()
    {
        Vector2 newPos = knight.transform.position - transform.position;
        // transform.position = Vector2.SmoothDamp(transform.position, newPos, ref veloc, 1);
        rigit.AddForce(newPos * force, ForceMode2D.Impulse  );
        Debug.Log("pet attack");
        isAttack = !isAttack;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            KnightStatic.instance.TakeDame(-10);
            collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
        }
    }
   
}
