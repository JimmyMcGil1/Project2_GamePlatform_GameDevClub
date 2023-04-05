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
    Vector2 pos;
    bool isCrouching;

    [Header("Crouch")]
    [SerializeField] float ySize;
    [SerializeField] float yOffset;
    Vector2 oldSize;
    Vector2 oldOff;

    [Header("Rolling")]
    [SerializeField] float rollTimmer;
    float rollCounter;
    bool isRoll;

    [SerializeField] float powerWallJump;
    public float powerJump;
    bool jump;
    bool onWall;
    Rigidbody2D rigit;
    BoxCollider2D box;
    Animator anim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float smoothness;

    float initialGravity;

    GameObject Dust;
    private void Awake()
    {
        if (this != null && this != instance) instance = this;
        box = GetComponent<BoxCollider2D>();
        rigit = GetComponent<Rigidbody2D>();
        jump = false;
        anim = GetComponent<Animator>();
        faceDir = transform.localScale;
        onWall = false;
        oldOff = box.offset;
        oldSize = box.size;
        isRoll = false;
        rollCounter = 0;
        rollTimmer = 1f;

        initialGravity = rigit.gravityScale;
        Dust = GameObject.FindGameObjectWithTag("Dust");
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("run", hor != 0 && IsGround() && !isCrouching  && !CheckSmallCollider());
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
        if (Input.GetKey(KeyCode.Space) && onWall) JumpingOnWall(powerWallJump);


        //Crouching
        if (Input.GetKey(KeyCode.S) )
        {
            
            Small_Collider();
            isCrouching = true;
            
        }
        else
        {
            isCrouching = false;
            box.size = oldSize;
            box.offset = oldOff;
        }
        
        anim.SetBool("run_crouch", !Mathf.Approximately(hor, 0) && isCrouching || CheckSmallCollider() && !Mathf.Approximately(hor, 0));
        anim.SetBool("isCrouch", isCrouching || CheckSmallCollider() );
        ///////////////

        //Roll 
        rollCounter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) && IsGround())
        {
            if (rollCounter >=  rollTimmer) 
            {
                isRoll = true;
                anim.SetTrigger("roll");
            }
        }

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
        //Roll
        if (isRoll)
        {
            rigit.AddForce(faceDir * 7, ForceMode2D.Impulse);
            isRoll = !isRoll;
            rollCounter = 0;
        }
    }
    public bool IsGround()
    {
        rigit.gravityScale = initialGravity;
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return hit.collider != null;
    }
    #region JumpOnWall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !IsGround())
        {
            onWall = true;
            Vector2 dir = collision.gameObject.transform.position - gameObject.transform.position;
            dir = dir.normalized;
            if (dir.x * faceDir.x < 0) faceDir.x = -faceDir.x;
            Debug.Log("wall");
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Dust.GetComponent<Animator>().SetTrigger("_dust");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (onWall)
        {
            Debug.Log("onwall");
            //    rigit.AddForce(Vector2.right * transform.localScale.x * 5f);
            anim.SetBool("wall_hang", true);
            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0.2f;
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            rigit.gravityScale = initialGravity;
            anim.SetBool("wall_hang", false);
            onWall = false;
        }
    }
    #endregion

    void JumpingOnWall(float v)
    {
        Vector2 newVelocity;
        newVelocity.x = (v + 5) * Mathf.Cos(70 * Mathf.Deg2Rad) * (-Mathf.Sign(faceDir.x));
        newVelocity.y = v * Mathf.Sin(70 * Mathf.Deg2Rad) - rigit.gravityScale * 1;
        rigit.velocity = newVelocity;
    }
    
    void Small_Collider()
    {
        box.size = new Vector2(box.size.x, ySize);
        box.offset = new Vector2(box.offset.x, yOffset);
    }
        
    bool CheckSmallCollider()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.up, 0.1f, wallLayer);
        if (hit.collider != null) Small_Collider();
        return hit.collider != null;
    }

   
}
