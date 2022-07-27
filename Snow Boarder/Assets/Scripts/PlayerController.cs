using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float rotateTorque = 15;
    float boostSpeed = 25;
    float regSpeed = 15;
    Rigidbody2D playerRB;
    SurfaceEffector2D ground;
    [SerializeField]
    ParticleSystem snowParticle;

    CrashDetector crashDetector;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        ground = GameObject.FindObjectOfType<SurfaceEffector2D>();
        crashDetector = GetComponent<CrashDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!crashDetector.crashed)
        {
            RotatePlayer();
            Boost();
        }
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            ground.speed = boostSpeed;
        }
        else
        {
            ground.speed = regSpeed;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("rotate right");
            playerRB.AddTorque(-rotateTorque, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("rotate left");
            playerRB.AddTorque(rotateTorque, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            snowParticle.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            snowParticle.Stop();
        }
    }


}
