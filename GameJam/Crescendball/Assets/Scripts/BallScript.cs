using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject         ball;
    public float                        force;
    public float                        speed;
    int                                 score;
    Rigidbody2D                         rb;
    Vector2                             direction;
    Vector2                             dir = new Vector2(1, -1);
    Vector2                             lastGoodVel;
    [SerializeField] List<GameObject> wall = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x != 0 && rb.velocity.y != 0)
        {
            lastGoodVel = rb.velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
            direction = rb.velocity.normalized;
            Transform transform = other.gameObject.GetComponent<Transform>();


            if (transform.localScale.x > transform.localScale.y)
            {
                rb.velocity = new Vector2(lastGoodVel.x, -lastGoodVel.y);
            }
            if (transform.localScale.y > transform.localScale.x)
            {
                rb.velocity = new Vector2(-lastGoodVel.x, lastGoodVel.y);
            }

            score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore(score += 100);
            speed = force * score;
        }

        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {

        }
    }
}
