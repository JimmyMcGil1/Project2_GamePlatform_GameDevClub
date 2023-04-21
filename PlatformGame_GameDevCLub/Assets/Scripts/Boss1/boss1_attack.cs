using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_attack : MonoBehaviour
{
    [SerializeField] Vector2 colliderRange;
    [SerializeField] float range;
    [SerializeField] LayerMask knightLayer;
    Vector2 realRange;
    BoxCollider2D box;
    AudioSource audi;
    [SerializeField] AudioClip attack1Clip;
    [SerializeField] AudioClip attack2Clip;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        realRange = colliderRange;
        realRange.x *= Mathf.Sign(transform.localScale.x);
    }
    public void Attack()
    {
        audi.PlayOneShot(attack1Clip);  
        Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)box.bounds.center + realRange, range, knightLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.CompareTag("Knight")) 
            {
                hit[i].GetComponent<KnightStatic>().TakeDame(-70);
            }

        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.magenta;
          Gizmos.DrawWireSphere((Vector2)box.bounds.center + realRange, range);
    }
    public void Skill_Attack(Transform knightPos)
    {
        //GetComponent<Animator>().SetTrigger("skill");
        Vector2 newPos = new Vector2();
        newPos.x = knightPos.transform.position.x + 0.5f;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
