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
    public float                        multiplier;
    float                               memoire;
    public int                          score;

    public bool                         hit;
    private bool                        playerHitTheBall;
    public bool PlayerHitTheBall { get => playerHitTheBall; set => playerHitTheBall = value; }

    public Vector2                      originalSpeed = new Vector2(2, -2);
    public Vector2                             lastGoodVel;
    private Animator                    anim;
    public Transform                    centerTransform;
    private int                         scoreBonus = 100;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        centerTransform = GameObject.Find("Center").GetComponent<Transform>();
    }

    void Start()
    {
        hit = false;
        multiplier = 0;
        countRebond = 0;
        rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(originalSpeed * force);
        multiplier = 1;
    }


    void Update()
    {
        
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            lastGoodVel = rb.velocity;
        }

        /*if((rb.velocity.x >= -0.2 && rb.velocity.x <= 0.2) || (rb.velocity.y >= -0.2 && rb.velocity.y <= 0.2))
        {
            if (!PlayerHitTheBall)
            {
                rb.velocity = (Vector2)centerTransform.position - rb.position * rb.velocity.magnitude;
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -60, 60), Mathf.Clamp(rb.velocity.y, -60, 60));
                
            }
        }*/
    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
            PlayerHitTheBall = false;


            Transform transform = other.gameObject.GetComponent<Transform>();

            speed = Mathf.Clamp(((force / 20) + (multiplier /5)), 1.0f, 40f);
    
                  //REBONDS
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

                //SCORING
            score = GameManager.Instance.GetScore();

            memoire = multiplier;

            if (countRebond >= 12 && PlayerHitTheBall == false)
            {
                countRebond += 0;
                
            }

            else if (countRebond >= 10 && PlayerHitTheBall)
            {
                countRebond = 0;
                int totalScore = scoreBonus * ((int)memoire * 2);
                GameManager.Instance.SetScore(score + totalScore);
                PopUpScore.Create(rb.position, totalScore);

            }



            else 
            { 
                countRebond++;
                multiplier++;
                int totalScore = scoreBonus * (int)memoire;
                GameManager.Instance.SetScore(score + totalScore);
                PopUpScore.Create(rb.position, totalScore);

            }




        }

       /*PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {

        }*/
    }
}
