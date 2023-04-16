using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_detach : MonoBehaviour
{
    [SerializeField] Door_beheviour door;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight")) {
            if (KnightStatic.instance.currLevel < 5) return;
            Debug.Log("knight hit door");
            StartCoroutine(door.Unlock());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            StartCoroutine(door.Lock());
        }
    }
}
