using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_behavior : MonoBehaviour
{
    Animator anim;
    public float attackRange;

    static public bool changeState;
    Transform knightPos;
    private void Awake()
    {
        anim = GetComponent<Animator>();

        changeState = false;
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
    }
    private void Update()
    {
        if ((knightPos.position.x - transform.position.x) * transform.localScale.x > 0)
        {
            Vector2 faceDir = transform.localScale;
            faceDir.x *= -1;
            transform.localScale = faceDir;
        }
        anim.SetBool("walk_state", !(Mathf.Abs(knightPos.position.x - transform.position.x) <= attackRange));
    }
    private void Start()
    {
        StartCoroutine(StartRun());
    }

    IEnumerator StartRun()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1);
        }
        anim.SetBool("walk_state", true);
    }
  
}
