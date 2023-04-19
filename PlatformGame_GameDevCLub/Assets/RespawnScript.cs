using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    GameObject player;
    [HideInInspector] public GameObject RespawnPoint;
    GameObject BOSS; 
    GameObject pet;
    [SerializeField] AudioSource RespawnSoundEffect;
    public static RespawnScript instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        player = GameObject.FindGameObjectWithTag("Knight");
        BOSS = GameObject.FindGameObjectWithTag("BOSS");
        RespawnPoint = GameObject.FindGameObjectWithTag("StartPoint");
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
     
        KnightStatic.instance.isDead = false;
        BOSS.SetActive(false);
        pet = GameObject.FindGameObjectWithTag("Pet");
        if (pet != null) Destroy(pet);
        GameManager.instance.PrintMessage("Shit! One more again? Can I actually dead?", 5);
        player.transform.position = RespawnPoint.transform.position;
        KnightStatic.instance.currHeal = KnightStatic.instance.maxHeal;
        KnightStatic.instance.currEXP = 0;
        KnightMoveset.instance.GetComponent<Animator>().SetTrigger("respawn");
        
        RespawnSoundEffect.Play();
    }
}
