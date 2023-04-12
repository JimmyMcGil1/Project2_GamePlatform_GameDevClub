using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;
    public GameObject RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (KnightStatic.instance.currHeal == 0)
        {
            player.transform.position = RespawnPoint.transform.position;
            GameManager.instance.GameOver();
            GameManager.instance.StartGame(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D respawn)
    {
        if (respawn.gameObject.CompareTag("Knight"))
        {
            player.transform.position = RespawnPoint.transform.position;
        }
    }
}
