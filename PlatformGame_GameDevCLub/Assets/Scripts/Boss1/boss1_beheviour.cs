using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_beheviour : MonoBehaviour
{
    Animator anim;

    [SerializeField] Transform initPos;
    [SerializeField] GameObject pet;
    static public bool changeState;
    [SerializeField] float amplitude;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
     
        changeState = false;
    }
    
    private void Start()
    {
        StartCoroutine(StartRun());
        StartCoroutine(StartSummon());
    }
  
    IEnumerator StartRun()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1);
        }
        anim.SetBool("run_state_1", true);
    }
    IEnumerator StartSummon()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
        }
        anim.SetTrigger("summon");
        Instantiate(pet, initPos.position, Quaternion.identity);
    }
   
}
