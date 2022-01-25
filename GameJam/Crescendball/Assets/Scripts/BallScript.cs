using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject         ball;
    Rigidbody2D                         rb;
    public float                        force;
    public float                        speed;
    public int                          countRebond;
    public int                          multiplyer;
    int                                 memoire;
    public int                          score;

    public bool                         hit;
    
    public Vector2                      originalSpeed;
    Vector2                             dir = new Vector2(1, -1);
    Vector2                             lastGoodVel;

    [SerializeField] List<GameObject> wall = new List<GameObject>();


    void Start()
    {
        hit = false;
        multiplyer = 0;
        countRebond = 0;
        rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force);
        originalSpeed = rb.velocity;
    }


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
            //speed = force * multiplyer;

            if (countRebond >= 10 && hit == false)
            {
                countRebond += 0;
                GameManager.Instance.SetScore(score += 0);
                memoire = multiplyer;
                multiplyer = 0;
            }

            else if (countRebond >= 10 && hit == true)
            {
                countRebond++;
                GameManager.Instance.SetScore(score += 100 * (memoire * 2));
            }

            else 
            { 
                countRebond++;
                multiplyer++;
                GameManager.Instance.SetScore(score += 100 * multiplyer); 
            }




        }

       /*PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {

        }*/
    }
}
