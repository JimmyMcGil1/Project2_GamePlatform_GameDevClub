using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveset : MonoBehaviour
{
    public static KnightMoveset instance { get; private set; }
    [Header("Move")]
    public float speed;
    float hor;
    Vector2 faceDir;
    public float powerJump;
    Vector2 pos;
    bool jump;
    bool onWall;
    Rigidbody2D rigit;
    BoxCollider2D box;
    Animator anim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    private void Awake()
    {
        if (this != null && this != instance) instance = this;
        box = GetComponent<BoxCollider2D>();
        rigit = GetComponent<Rigidbody2D>();
        jump = false;
        anim = GetComponent<Animator>();
        faceDir = transform.localScale;
        onWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("run", hor != 0 && IsGround());
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
     /*   if (OnWall())
        {
            onWall = true;
        } 
    */
    }
    private void FixedUpdate()
    {
        //Moving
        if (hor != 0)
        {
            pos = rigit.position;
            pos.x += hor * speed * Time.deltaTime;
            rigit.position = pos;
            if (faceDir.x * hor < 0) faceDir.x = -faceDir.x;
        }
        transform.localScale = faceDir;

        //////////

        //Jumping
        if (jump && IsGround())
        {
            rigit.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            jump = false;
            anim.SetTrigger("jump");

        }
    /*    if (onWall)
        {
            Debug.Log("onwall");
            rigit.AddForce(Vector2.right * transform.localScale.x * 5f);
            anim.SetTrigger("onWall");

            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0;
            if (Input.GetKeyDown(KeyCode.Space)) JumpingOnWall();
            onWall = false;
        }
        else
        {
            rigit.gravityScale = 6;
        }
    */
    }
    public bool IsGround()
    {
        Debug.Log("isground");
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return hit.collider != null;
    }
    bool OnWall()
    {
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.05f);
        return (hitWall.collider != null && hitWall.collider.CompareTag("Wall"));
    }
    void JumpingOnWall()
    {
        rigit.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * powerJump * 20, powerJump * 30 * Mathf.Sin(60 * Mathf.Deg2Rad)));
    }
}
