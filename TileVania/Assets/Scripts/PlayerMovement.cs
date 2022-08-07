using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D playerRB;
    SpriteRenderer playerSprite;
    Animator playerAnimator;


    [SerializeField]
    float runSpeed = 5;

    [SerializeField]
    float jumpForce = 50;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = Physics2D.gravity * 5;
        playerRB = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    private void Jump()
    {
        if (playerRB.velocity.y < -.1)
        {
            playerAnimator.SetBool("IsFalling", true);
            playerAnimator.SetBool("IsJumping", false);
        }
        else
        {
            playerAnimator.SetBool("IsFalling", false);

        }
    }

    void Run()
    {
        Vector2 playerVel = new Vector2(moveInput.x*runSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVel;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //player is moving
        if (moveInput.x != 0)
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
        playerAnimator.SetBool("IsJumping", true);
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }
}
