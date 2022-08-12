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
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject gun;

    Vector2 deathPos = Vector2.zero;

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

    [SerializeField]
    float ySpeedCap = 25f;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();

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
            if (playerRB.velocity.y > ySpeedCap) playerRB.velocity = new Vector2(playerRB.velocity.x, ySpeedCap);
        }
        else
        {
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector2.up;
            playerCollider.isTrigger = true;
            feetCollider.isTrigger = true;
            playerSprite.color = new Color(255, 255, 255,.5f);
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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
            transform.localScale = new Vector3(Math.Sign(moveInput.x), 1, 1);
            
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

    }

    void OnJump()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive){
            playerAnimator.SetBool("IsJumping", true);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Hazards") 
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        Instantiate(bullet, gun.transform.position, gun.transform.rotation);
    }
}
