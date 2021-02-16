using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerMovement : MonoBehaviour
{
    //Movement Field
    [SerializeField] private float moveSpeed;
    public KeyCode jump;
    public KeyCode down;
    public bool P1;
    public bool P2;
    private float moveX;

    //JumpField
    [SerializeField] private float jumpForce;
    
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    [SerializeField] private float groundDelay;
    private float rememberGroundTime = 0;

    //Ladder Field
    public bool onLadder;
    public bool ladderBelow;
    [SerializeField] private float climbSpeed = 1.0f;
    private float initGravity;

    [SerializeField] private Vector3 platformCheckOffset;
    [SerializeField] private float platformCheckRadius;
    [SerializeField] private LayerMask platformLayer;

    //OneWayPlatform Field
    public bool onPlatform;
    public bool jumpingThroughPlatform = false;
    public float jumpThroughDelay;

    //Misc
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] private Collider2D environmentCollider;
    [SerializeField] private Collider2D platformCollider;
    [SerializeField] private Collider2D hitCollider;

    private PlatformerInteraction pi;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>() ? GetComponent<SpriteRenderer>() : null;

        initGravity = rb.gravityScale;

        pi = GetComponent<PlatformerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlatformCheck();

        //Movement Input
        JumpInput();
        LadderInput();
        OneWayPlatformInput();

        Movement();
    }

    private void OneWayPlatformInput()
    {
        if (onPlatform)
        {
            if (Input.GetKeyDown(down))
            {
                if (!pi.canInteract)
                {
                    StartCoroutine(ResetPlatformCheck());
                    platformCollider.enabled = false;
                }
            }
        }
    }
  
    private void Movement()
    {
        if (P1)
        {
            moveX = Input.GetAxisRaw("Horizontal_P1") * moveSpeed;
            rb.velocity = new Vector2(moveX, rb.velocity.y);
        }
        else if (P2)
        {
            moveX = Input.GetAxisRaw("Horizontal_P2") * moveSpeed;
            rb.velocity = new Vector2(moveX, rb.velocity.y);
        }
        //float xVelo = moveX * Time.deltaTime;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(jump) && !onLadder)
        {
            Jump();
        }
    }

    private void LadderInput()
    {
        if (onLadder)
        {
            platformCollider.enabled = false;
            if (Input.GetKey(jump))
            {
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(down) && ladderBelow)
            {
                rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            rb.gravityScale = 0;
            
        }
        else
        {
            rb.gravityScale = initGravity;
        }

        if (ladderBelow)
        {
            if (Input.GetKey(down) && !onLadder)
            {
                platformCollider.enabled = false;
                rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
            }
        }
    }

    private void Jump()
    {
        if (!isGrounded) return;
        float yVelo = jumpForce;
        rb.velocity = new Vector2(rb.velocity.x, yVelo);
    }

    private void PlatformCheck()
    {
        RaycastHit2D platform = Physics2D.Raycast(transform.position + platformCheckOffset, -transform.up, platformCheckRadius, platformLayer);

        if (platform)
        {
            onPlatform = true;
        }
        else
        {
            onPlatform = false;
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position + groundCheckOffset, -transform.up, groundCheckRadius, groundLayer);
        
        if (ground)
        {
            isGrounded = true;
            rememberGroundTime = groundDelay;
        }
        else
        {
            if(rememberGroundTime > 0)
            {
                rememberGroundTime -= Time.deltaTime;
            }

            if(rememberGroundTime <= 0)
            {
                isGrounded = false;
            }
        }

        if (!jumpingThroughPlatform)
        {
            platformCollider.enabled = isGrounded;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + groundCheckOffset, transform.position + groundCheckOffset + -transform.up * groundCheckRadius);
    }

    IEnumerator ResetPlatformCheck()
    {
        jumpingThroughPlatform = true;
        yield return new WaitForSeconds(jumpThroughDelay);
        jumpingThroughPlatform = false;
    }
}
