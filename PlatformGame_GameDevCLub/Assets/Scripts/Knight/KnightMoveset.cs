using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveset : MonoBehaviour, IDataPersistence
{
    public static KnightMoveset instance { get; private set; }
    [Header("Move")]
    float speed;
    float hor;
    Vector2 faceDir;
   
    [Header("Rolling")]
    [SerializeField] float rollForce;
    bool rolling;
    float rollTimmer;
    float rollCounter;

    [Header("Jump")]
    public float powerJump;
    Vector2 pos;
    bool jump;
    bool onWall;

    [Header("Crouching")]
    [SerializeField] Vector2 newSize;
    [SerializeField] Vector2 newOffset;
    bool crouch;
    Vector2 oldSize;
    Vector2 oldOffset;
    
    Rigidbody2D rigit;
    BoxCollider2D box;
    Animator anim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    float initialGravity;
    [SerializeField] float powerJumpOnWall;
    [SerializeField] float smoothScale;

    GameObject dust;


    private void Awake()
    {
        if (this != null && this != instance) instance = this;
        box = GetComponent<BoxCollider2D>();
        rigit = GetComponent<Rigidbody2D>();
        jump = false;
        anim = GetComponent<Animator>();
        faceDir = transform.localScale;
        onWall = false;
        initialGravity = rigit.gravityScale;
        rolling = true;
        rollTimmer = 0.5f;
        rollCounter = 0f;
        oldOffset = box.offset;
        oldSize = box.size;
        dust = GameObject.FindGameObjectWithTag("Dust");
        //Set the init static of knigt
    }
    private void Start()
    {
        this.speed = KnightStatic.instance.speed;
    }
    // Update is called once per frame
    void Update()
    {
        //Moving
        this.speed = KnightStatic.instance.speed;
        hor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("run", hor != 0 && IsGround() );
       
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
        if (Input.GetKeyDown(KeyCode.Space) && onWall)
        {
            JumpingOnWall(powerJumpOnWall);
        }
        anim.SetBool("wall_hang", onWall);
        
        //Rolling
        if (Input.GetKeyDown(KeyCode.C) && IsGround())
        {
            if (rollCounter > rollTimmer)
            {
                rolling = true;
                Crouching();
                anim.SetTrigger("roll");
                rollCounter = 0;
            }
            box.size = oldSize;
            box.offset = oldOffset;

        }
        rollCounter += Time.deltaTime;

        //Crouching
        if ((Input.GetKey(KeyCode.S) || (Input.GetKeyDown(KeyCode.S)) ) && Mathf.Approximately(hor, 0f))
        {
            if (Input.GetKeyDown(KeyCode.S))
                rigit.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
              Crouching();
              crouch = true;
        }
        else
        {
            crouch = false;
            box.size = oldSize;
            box.offset = oldOffset;
        }
        anim.SetBool("crouch", crouch && hor == 0);
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
        
        if (rolling)
        {
            rigit.AddForce(new Vector2(rollForce * 1 * faceDir.x, 0), ForceMode2D.Impulse);
            rolling = !rolling;
        }

        if (crouch)
        {
        }
        
    }
    public bool IsGround()
    {
        rigit.gravityScale = initialGravity;
       // Debug.Log("isground");
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return hit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !IsGround())
        {
            
            onWall = true;
            float dir = collision.gameObject.transform.position.x - transform.position.x;
            if (faceDir.x * dir < 0) faceDir.x = -faceDir.x;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            dust.GetComponent<Animator>().SetTrigger("_dust");
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (onWall)
        {
            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0.4f;
        }
        else return;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (onWall)
        {
            rigit.gravityScale = initialGravity;
            onWall = !onWall;
        }
      
    }
    void JumpingOnWall(float powerJump)
    {
        Vector2 newPos;
        newPos.x = (powerJump  ) *   Mathf.Cos(60 * Mathf.Deg2Rad) *  (-Mathf.Sign(faceDir.x));
        newPos.y = (powerJump + 10 ) *   Mathf.Sin(60 * Mathf.Deg2Rad) ;
        rigit.AddForce(newPos , ForceMode2D.Impulse);
        onWall = !onWall;

    }
    void Crouching()
    {
        box.size = newSize;
        box.offset = newOffset;
    }
    public void LoadData(GameData gameData)
    {
        transform.position = gameData.playerPos;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.playerPos = transform.position;
    }
}
