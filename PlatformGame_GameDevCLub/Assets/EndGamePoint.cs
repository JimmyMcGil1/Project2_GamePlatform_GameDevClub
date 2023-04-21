using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePoint : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            GameManager.instance.PrintMessage("Now The King is free from immortality. All the thing is gone through the wind!", 6f);
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(StartEndGame(6));

        }
    }
    IEnumerator StartEndGame(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        GameManager.instance.Quit();
    } 
}
