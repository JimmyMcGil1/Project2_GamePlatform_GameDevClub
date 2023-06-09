using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject fireball;
    public Transform fireballPos;
    private float timer;
    public float spawnRate;
    public float realDistance;
    private GameObject player;
    [SerializeField] AudioSource fireballSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Knight");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < realDistance)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0;
                shoot();
            }
        }

        
    }
    void shoot()
    {
        Instantiate(fireball, fireballPos.position, Quaternion.identity);
        fireballSoundEffect.Play();
    }
}
