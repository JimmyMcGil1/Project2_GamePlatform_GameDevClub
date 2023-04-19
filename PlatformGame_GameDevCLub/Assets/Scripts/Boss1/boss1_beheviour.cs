using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_beheviour : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject pet;
    [SerializeField] Transform initPos;
    static public bool changeState;
    [SerializeField] float amplitude;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //pet.transform.position = initPos.position;
       // pet.SetActive(false);
        changeState = false;
    }

    
    public IEnumerator StartRun()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1);
        }
        anim.SetBool("run_state_1", true);
    }
    public IEnumerator StartSummon()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
        }
        anim.SetTrigger("summon");
        Instantiate(pet, initPos.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            KnightStatic.instance.TakeDame(-20);
            // collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
