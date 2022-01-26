using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject         ball;
    [SerializeField] Animator         interfaceScore;
    Rigidbody2D                         rb;
    public float                        force;
    public float                        speed;
    public int                          countRebond;
    public float                        multiplier;
    float                               memoire;
    public int                          score;

    public bool                         hit;
    private bool                        playerHitTheBall = false;
    public bool                         PlayerHitTheBall { get => playerHitTheBall; set => playerHitTheBall = value; }

    public Vector2                      originalSpeed = new Vector2(2, -2);
    public Vector2                             lastGoodVel;
    private Animator                    anim;
    public Transform                    centerTransform;
    private int                         scoreBonus = 100;
    public int                          multiplierDuo = 1;
    private bool otheranim;

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
        rb.AddForce(originalSpeed * 150);
        multiplier = 1;
        speed = force;
    }


    void Update()
    {
        
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            lastGoodVel = rb.velocity;
            if (PlayerHitTheBall)
            {
                Debug.Log(lastGoodVel);
            }
        }

        if (PlayerHitTheBall)
        {
            speed *= 1.2f;
            PlayerHitTheBall = false;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
           






           
           interfaceScore.SetTrigger("playScore");

            
            multiplierDuo++;
            Transform transform = other.gameObject.GetComponent<Transform>();


            ///// REBONDS /////
            AudioManager.instance.Play("Bounce");
            if (transform.localScale.x > transform.localScale.y)
            {
                rb.velocity = new Vector2 (Mathf.Clamp(lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x) * speed, -55, 55), Mathf.Clamp(-lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -55, 55)) ;
                if(lastGoodVel.y < rb.velocity.y)
                {
                    anim.SetTrigger("Splash Up");
                }
                else
                {
                    anim.SetTrigger("Splash Down");
                }

                anim.SetFloat("Speed", speed / 2);
            }

            if (transform.localScale.y > transform.localScale.x)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x)* speed, -55, 55) , Mathf.Clamp(lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -55, 55));
                if (lastGoodVel.x < rb.velocity.x)
                {
                    anim.SetTrigger("Splash Right");
                }
                else
                {
                    anim.SetTrigger("Splash Left");
                }
            }

            ///// SCORING /////

            score = GameManager.Instance.GetScore();
            memoire = multiplier;


            if (countRebond >= 18 && PlayerHitTheBall == false)
            {
                interfaceScore.SetBool("playScoreMaxed", true);
                otheranim = true;
                Debug.Log("vitesse maximale");

            }
            else
            {
                interfaceScore.SetBool("playScoreMaxed", false);
                countRebond++;
                int totalScore = scoreBonus * (int)memoire;
                PopUpScore.Create(rb.position, totalScore, (int)multiplier);
                if (multiplierDuo == 3)
                {

                    multiplier++;
                    multiplierDuo = 1;
                    speed += multiplier / 6;
                }
                GameManager.Instance.SetScore(score + totalScore);


            }
            GameManager.Instance.SetMult((int)multiplier);

        }

    }
}
