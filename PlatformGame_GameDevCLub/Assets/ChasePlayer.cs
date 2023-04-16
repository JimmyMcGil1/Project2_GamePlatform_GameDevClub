using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform StartingPoint;
    [SerializeField] AudioSource ChasingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Knight");
        ChasingPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (chase == true)
            Chase();
        else 
            ReturnStartPoint();
        Flip();
        
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, StartingPoint.position, speed *  Time.deltaTime);
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knight")
        {
            KnightStatic.instance.TakeDame(-10);
        }
    }
}
