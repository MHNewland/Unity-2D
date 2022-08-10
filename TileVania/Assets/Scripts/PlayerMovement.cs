using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Vector2 playerVel;
    Rigidbody2D playerRB;
    SpriteRenderer playerSprite;
    Animator playerAnimator;
    BoxCollider2D playerCollider;


    [SerializeField]
    float runSpeed = 5;

    [SerializeField]
    float climbSpeed = 5;
    

    [SerializeField]
    float jumpForce = 50;

    [SerializeField]
    float playerGravity = 5;

    [SerializeField]
    bool isAlive;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerRB.gravityScale = playerGravity;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Run();
            Jump();
            Climb();
        }

    }

    private void Climb()
    {

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerAnimator.SetBool("IsJumping", false);
            playerAnimator.SetBool("IsFalling", false);
            playerRB.velocity = new Vector2(playerRB.velocity.x, moveInput.y * climbSpeed);
            playerRB.gravityScale = 0;

            if (moveInput.y != 0)
            {
                playerAnimator.SetBool("IsClimbing", true);


            }
            else
            {
                playerAnimator.SetBool("IsClimbing", false);
            }
        }
        else
        {

            playerRB.gravityScale = playerGravity;
            playerAnimator.SetBool("IsClimbing", false);
        }
    }

    private void Jump()
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (playerRB.velocity.y < -.1)
            {
                playerAnimator.SetBool("IsJumping", false);
                playerAnimator.SetBool("IsFalling", true);
            }
            else
            {
                playerAnimator.SetBool("IsFalling", false);

            }
        }
    }
        void Run()
    {
        playerVel = new Vector2(moveInput.x*runSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVel;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //player is moving
        if (moveInput.x != 0 && isAlive)
        {
            playerAnimator.SetBool("IsRunning", true);

            //if moving left, flip sprite
            if (moveInput.x < 0)
            {
                playerSprite.flipX = true;
            }
            else if (moveInput.x > 0)
            {
                playerSprite.flipX = false;
            }
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

    }

    void OnJump()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive){
            playerAnimator.SetBool("IsJumping", true);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Debug.Log("dead");
            isAlive = false;
        }
    }
}
