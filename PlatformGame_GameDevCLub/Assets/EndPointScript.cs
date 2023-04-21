using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour
{
    [SerializeField] int currLevel; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            GameManager.instance.PrintMessage("Finaly a nightmare is over. What is waiting for my next?", 4f);
            KnightMoveset.instance.GetComponent<Animator>().SetTrigger("level_up");
            StartCoroutine(CountDownLoad(5));
        }
    }
    IEnumerator CountDownLoad(int sec)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        EndPointList.instance.LoadNextScene(currLevel);
    }
}
