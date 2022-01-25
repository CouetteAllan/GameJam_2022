using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject         ball;
    Rigidbody2D                         rb;
    public float                        force;
    public float                        speed;
    public int                          countRebond;
    public float                        multiplyer;
    float                               memoire;
    public int                          score;

    public bool                         hit;
    private bool                        playerHitTheBall;
    public bool PlayerHitTheBall { get => playerHitTheBall; set => playerHitTheBall = value; }

    public Vector2                      originalSpeed = new Vector2(2, -2);
    public Vector2                             lastGoodVel;
    private Animator                    anim;
    public Transform centerTransform;


    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        centerTransform = GameObject.Find("Center").GetComponent<Transform>();
    }

    void Start()
    {
        hit = false;
        multiplyer = 0;
        countRebond = 0;
        rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(originalSpeed * force);
    }


    void Update()
    {
        
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            lastGoodVel = rb.velocity;
            //lastGoodPos = ballPos.position;
        }

        if((rb.velocity.x >= -0.2 && rb.velocity.x <= 0.2) || (rb.velocity.y >= -0.2 && rb.velocity.y <= 0.2))
        {
            if (!PlayerHitTheBall)
            {
                rb.velocity = (Vector2)centerTransform.position - rb.position * rb.velocity.magnitude;
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -60, 60), Mathf.Clamp(rb.velocity.y, -60, 60));
                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
            PlayerHitTheBall = false;


            Transform transform = other.gameObject.GetComponent<Transform>();

            speed = Mathf.Clamp(((force / 20) + (multiplyer /5)), 1.0f, 40f);

            if (transform.localScale.x > transform.localScale.y)
            {
                
                rb.velocity = new Vector2 (Mathf.Clamp(lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x) * speed, -60, 60), Mathf.Clamp(-lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -60, 60)) ;
                if(lastGoodVel.y < rb.velocity.y)
                {
                    anim.SetTrigger("Splash Up");
                }
                else
                {
                    anim.SetTrigger("Splash Down");
                }

                anim.SetFloat("Speed", speed);
            }

            if (transform.localScale.y > transform.localScale.x)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x)* speed, -60, 60) , Mathf.Clamp(lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -60, 60));
                if (lastGoodVel.x < rb.velocity.x)
                {
                    anim.SetTrigger("Splash Right");
                }
                else
                {
                    anim.SetTrigger("Splash Left");
                }
            }


            score = GameManager.Instance.GetScore();

            if (countRebond >= 10 && hit == false)
            {
                countRebond += 0;
                GameManager.Instance.SetScore(score += 0);
                memoire = multiplyer;
                //multiplyer = 1;
            }

            else if (countRebond >= 10 && hit == true)
            {
                countRebond = 0;
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
