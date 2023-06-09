using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveset : MonoBehaviour, IDataPersistence
{
    public static KnightMoveset instance { get; private set; }
    [Header("Move")]
    int speed;
    float hor;

    [HideInInspector]
    public Vector2 faceDir;
   
    [Header("Rolling")]
    [SerializeField] float rollForce;
    bool rolling;
    float rollTimmer;
    float rollCounter;

    [Header("Jump")]
    [SerializeField] float powerJumpOnWall;
    public float powerJump;
    [SerializeField] Vector2 wallCollider;
    [SerializeField] Vector2 wallColliderPos;
    Vector2 center;
    
    Vector2 pos;
    bool jump;
    
    [HideInInspector]
    public bool onWall;

    [Header("Crouching")]
    [SerializeField] Vector2 newSize;
    [SerializeField] Vector2 newOffset;
    bool crouch;
    Vector2 oldSize;
    Vector2 oldOffset;
    
    Rigidbody2D rigit;
    [HideInInspector] public BoxCollider2D box;
    Animator anim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    float initialGravity;
    [SerializeField] float smoothScale;
    [SerializeField] private AudioSource jumpingSoundEffect;
    [SerializeField] private AudioSource rollingSoundEffect;

    GameObject dust;
    bool isGround;

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
        isGround = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGround()) jump = true;
        if (Input.GetKeyDown(KeyCode.Space) && onWall && !IsGround())
        {
            JumpingOnWall(powerJumpOnWall);
        }
        anim.SetBool("wall_hang", onWall);
        
        //Rolling
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (rollCounter > rollTimmer)
            {
                rolling = true;
                anim.SetTrigger("roll");
                //StartCoroutine(InCrouching(3f));
                rollCounter = 0;
                rollingSoundEffect.Play();
            }
           

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


        center = box.bounds.center;
        center.y += wallColliderPos.y;
        center.x = (faceDir.x > 0) ? (center.x + wallColliderPos.x) : (center.x - wallColliderPos.x);
    }
    private void FixedUpdate()
    {
        //Moving
        if (hor != 0 && !KnightStatic.instance.isDead)
        {
            pos = rigit.position;
            pos.x += hor * speed * Time.deltaTime;
            rigit.position = pos;
            if (faceDir.x * hor < 0) faceDir.x = -faceDir.x;
        }
        transform.localScale = faceDir;

        //////////

        //Jumping
        if (jump && IsGround() && !KnightStatic.instance.isDead)
        {
            
            rigit.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            jump = false;
            anim.SetTrigger("jump");
            jumpingSoundEffect.Play();

        }
        
        if (rolling)
        {
            rigit.AddForce(Vector2.right * Mathf.Sign(faceDir.x) * rollForce, ForceMode2D.Impulse);
            rolling = !rolling;
        }
    }
    public bool IsGround()
    {
        rigit.gravityScale = initialGravity;
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
   
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.blue;
        wallColliderPos.x *= Mathf.Sign(faceDir.x);
        Gizmos.DrawWireCube(center, wallCollider );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !IsGround())
        {
            
            onWall = true;
            float dir = collision.gameObject.transform.position.x - transform.position.x;
            if (faceDir.x * dir < 0) faceDir.x = -faceDir.x;
        }
         if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            dust.GetComponent<Animator>().SetTrigger("_dust");
        }

    }
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (onWall)
        {
            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0.6f;
        }
        else return;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rigit.gravityScale = initialGravity;
        if (onWall)
        {
            Debug.Log("fall");
            onWall = !onWall;
            anim.SetTrigger("fall");
        }
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGround = false;
        }
    }
    void JumpingOnWall(float powerJump)
    {
        Vector2 newPos;
        newPos.x = (powerJump  ) *   Mathf.Cos(70 * Mathf.Deg2Rad) *  (-Mathf.Sign(faceDir.x));
        newPos.y = (powerJump + 8 ) *   Mathf.Sin(70 * Mathf.Deg2Rad) ;
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
        speed = gameData.speed;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.playerPos = transform.position;
        gameData.speed = speed;
    }
    
}
