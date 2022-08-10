using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D enemyRB;
    BoxCollider2D reversePeri;

    [SerializeField]
    float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        reversePeri = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
