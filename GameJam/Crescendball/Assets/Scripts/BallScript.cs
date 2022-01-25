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
    public float                          multiplyer;
    float                                 memoire;
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
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            lastGoodVel = rb.velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
            Transform transform = other.gameObject.GetComponent<Transform>();

            speed = Mathf.Clamp(((force / 100) + (multiplyer /10)), 1.0f, 1.8f);

            if (transform.localScale.x > transform.localScale.y)
            {
                rb.velocity = new Vector2 (Mathf.Clamp(lastGoodVel.x, -60, 60), Mathf.Clamp(-lastGoodVel.y, -60, 60)) * speed;
            }

            if (transform.localScale.y > transform.localScale.x)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-lastGoodVel.x, -60, 60) , Mathf.Clamp(lastGoodVel.y, -60, 60)) * speed;
            }


            score = GameManager.Instance.GetScore();

            if (countRebond >= 10 && hit == false)
            {
                countRebond += 0;
                GameManager.Instance.SetScore(score += 0);
                memoire = multiplyer;
                multiplyer = 1;
            }

            else if (countRebond >= 10 && hit == true)
            {
                countRebond++;
                GameManager.Instance.SetScore(score += 100 * ((int)memoire * 2));
            }

            else 
            { 
                countRebond++;
                multiplyer++;
                GameManager.Instance.SetScore(score += 100 * (int)multiplyer); 
            }




        }

       /*PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {

        }*/
    }
}
