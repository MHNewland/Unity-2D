using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 20f;
    float rotateSpeed = 90f;
    bool hasPackage;
    SpriteRenderer carSprite;

    [SerializeField]
    int score;

    private void Start()
    {
        score = 0;
        carSprite = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, Input.GetAxis("Vertical")) *Time.deltaTime * speed,Space.Self);
        this.transform.Rotate(Vector3.back, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        speed = 30f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        switch (collision.tag)
        {
            case "Boost":
                {
                    Debug.Log("boost");
                    speed = 50f;
                    break;
                }
            case "Package":
                {
                    Debug.Log("Package:" + hasPackage);
                    if (!hasPackage)
                    {
                        carSprite.color = collision.gameObject.GetComponent<SpriteRenderer>().color;
                        Destroy(collision.gameObject);
                        hasPackage = true;
                    }
                    break;
                }
            case "DropOff":
                {
                    Debug.Log("DropOff:" + hasPackage);
                    if (hasPackage)
                    {
                        carSprite.color = Color.white;
                        score++;
                        Debug.Log("Score: " + score);
                        hasPackage = false;
                    }
                    break;
                }
            default: break;
        }

    }
}
