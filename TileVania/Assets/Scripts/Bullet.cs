using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D bulletRB;
    float xSpeed;


    [SerializeField]
    float bulletSpeed;

    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x* bulletSpeed;
        transform.localScale = new Vector3(player.transform.localScale.x * transform.localScale.x,
                                        transform.localScale.y,
                                        transform.localScale.z);

        bulletRB.angularVelocity = -1440f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = new Vector2(xSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
