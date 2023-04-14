using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;
    public GameObject RespawnPoint;
    public static RespawnScript instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (KnightStatic.instance.isDead)
        {
          //  GameManager.instance.GameOver();
         //   player.transform.position = RespawnPoint.transform.position;
         //   GameManager.instance.StartGame(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D respawn)
    {
        if (respawn.gameObject.CompareTag("Knight"))
        {
            player.transform.position = RespawnPoint.transform.position;
        }
    }
    public void Respawn()
    {
        GameManager.instance.PrintMessage("Shit! One more again? Can I actually dead?", 5);
        player.transform.position = RespawnPoint.transform.position;
        KnightStatic.instance.currHeal = KnightStatic.instance.maxHeal;
        KnightStatic.instance.currEXP = 0;
        KnightMoveset.instance.GetComponent<Animator>().SetTrigger("respawn");
    }
}
