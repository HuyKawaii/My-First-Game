using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioManager audioManager;
    Rigidbody2D playerRB;
    float horizontalInput;
    float verticalInput;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;
    Vector3 localScale;
    Animator animator;
    public static bool isAlive = true;

    [SerializeField] LayerMask platform;
    RaycastHit2D groundCheck;
    float rayLength = 0.0f;
    float maxFallSpeed = 15.0f;
    bool isOnGround;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        isOnGround = true;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (isAlive && !PauseMenuManager.isGamePause)
        {
            Walk();
            Jump();
            Attacking();
            Flip();
            Animation();
            Grounded();
        }
    }

    private void Walk()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(horizontalInput * playerSpeed, playerRB.velocity.y);
        localScale = transform.localScale;

        if (playerRB.velocity.x != 0 && !audioManager.footstep.isPlaying && isOnGround)
        {
            audioManager.Footstep();
        }

        if (playerRB.velocity.x == 0 || !isOnGround)
        {
            audioManager.footstep.Pause();
        }
    }

    private void Jump()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0 && isOnGround)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);
            audioManager.Jump();
        }

        //keep player from falling too fast
        if (playerRB.velocity.y < -maxFallSpeed)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -maxFallSpeed, Mathf.Infinity));
        }
    }
    
    private void Flip()
    {
        if (horizontalInput < 0)
        {
            localScale.x = -1;
        }else if (horizontalInput > 0)
        {
            localScale.x = 1;
        }
        transform.localScale = localScale;
    }

    private void Attacking()
    {
        if (playerCombat.isAttacking)
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            horizontalInput = 0;
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        gameObject.layer = 8;
    }
    private void Animation()
    {
        animator.SetFloat("Velocity_x", Mathf.Abs(playerRB.velocity.x));
        animator.SetFloat("Velocity_y", playerRB.velocity.y);
        animator.SetFloat("IsAttacking", playerCombat.cooldown);
        animator.SetBool("IsOnGround", isOnGround);
    }

    private void Grounded()
    {
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, rayLength, platform);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.yellow);
        if (groundCheck.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
}
