using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_behavior : MonoBehaviour
{
    Animator anim;

    static public bool changeState;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        changeState = false;
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
