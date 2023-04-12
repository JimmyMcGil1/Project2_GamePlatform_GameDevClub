using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell_beheviour : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim.SetTrigger("spell");
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Knight"))
        {

            KnightStatic.instance.TakeDame(-10);
        }                
    }
    IEnumerator HoldDestroy()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(2);
        }
        Destroy(gameObject);
    }
}
