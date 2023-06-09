using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject knight;
    Rigidbody2D rigit;
    BoxCollider2D box;
    Vector2 newPos; 
    [SerializeField]float speed ;
    Animator anim;
    int dir;
    private void Awake()
    {
        knight = GameObject.FindGameObjectWithTag("Knight");
        rigit = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        box.enabled = true;
        if (gameObject.transform.position.x - knight.transform.position.x > 0) dir = 1;
        else
        {
            dir = -1;
            Vector2 newDir = gameObject.transform.localScale;
            newDir.x += -1;
            gameObject.transform.localScale = newDir;
        }
        StartCoroutine(HoldDisapear());
    }
    private void Update()
    {
        Fire(dir);
    }
    void Fire(int dir)
    {
        newPos = rigit.position;
        newPos.x += dir * speed * Time.deltaTime;
        rigit.MovePosition(newPos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.gameObject.GetComponent<Boss2_static>().TakeDame(-100);
        }
        if (collision.gameObject.CompareTag("Boss3"))
        {
            collision.gameObject.GetComponent<Boss3_static>().TakeDame(-100);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemy_static>().TakeDame(-100);
        }
        anim.SetTrigger("explode");
        speed = 0;
        StartCoroutine(HoldDestroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Boss1_static>().TakeDame(-100);
            anim.SetTrigger("explode");
            speed = 0;
            StartCoroutine(HoldDestroy());
        }
    }
    IEnumerator HoldDestroy()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
    IEnumerator HoldDisapear()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(10f);
        }
        Destroy(gameObject);
    }
}

