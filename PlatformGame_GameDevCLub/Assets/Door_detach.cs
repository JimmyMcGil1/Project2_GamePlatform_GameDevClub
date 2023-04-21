using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_detach : MonoBehaviour
{
    [SerializeField] Door_beheviour door;
    [SerializeField] float countDownToLock;
    BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight")) {
            if (KnightStatic.instance.currLevel >= 5)
            {
                Debug.Log("knight hit door");
                StartCoroutine(door.Unlock());
                StartCoroutine(StartLock(2.5f));
            }
           
        }
    }

        IEnumerator StartLock(float sec)
    {
        for (int i = 0; i <= 1; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        StartCoroutine(door.Lock());
    }
}
