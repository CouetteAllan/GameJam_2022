using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject         ball;
    [SerializeField] Animator           interfaceScore;
    [SerializeField] Animator           cam;
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
    private SpriteRenderer sprite;
    private Color spriteColor;
    private ParticleSystem particles;
    private int                         scoreBonus = 100;
    public int                          multiplierDuo = 1;
    private int nbrOfInvincincibleBounces = 0; //pour dire que le joueur se fait pas toucher par la balle pendant 1 ou 2 rebonds pour �viter la frustration

    private bool otheranim;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        anim = this.GetComponent<Animator>();
        centerTransform = GameObject.Find("Center").GetComponent<Transform>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        spriteColor = sprite.color;
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
            Physics2D.IgnoreLayerCollision(3, 7, true);
            nbrOfInvincincibleBounces = 0;
            sprite.color = Color.green;
            PlayerHitTheBall = false;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
           
           
           interfaceScore.SetTrigger("playScore");

            nbrOfInvincincibleBounces++;
            multiplierDuo++;
            Transform transform = other.gameObject.GetComponent<Transform>();
            if(nbrOfInvincincibleBounces >= 3)
            {
                Physics2D.IgnoreLayerCollision(3, 7, false);
                sprite.color = spriteColor;
                particles.Stop();
            }

            ///// REBONDS /////
            AudioManager.instance.Play("Bounce");
            if (transform.localScale.x > transform.localScale.y)
            {
                rb.velocity = new Vector2 (Mathf.Clamp(lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x) * speed, -55, 55), Mathf.Clamp(-lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -55, 55)) ;
                if(lastGoodVel.y < rb.velocity.y)
                {
                    anim.SetTrigger("Splash Up");
                    cam.SetTrigger("CamShakeTop");
                }
                else
                {
                    anim.SetTrigger("Splash Down");
                    cam.SetTrigger("CamShakeBot");

                }

                anim.SetFloat("Speed", speed / 2);
            }

            if (transform.localScale.y > transform.localScale.x)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-lastGoodVel.normalized.x * Mathf.Abs(originalSpeed.x)* speed, -55, 55) , Mathf.Clamp(lastGoodVel.normalized.y * Mathf.Abs(originalSpeed.y) * speed, -55, 55));
                if (lastGoodVel.x < rb.velocity.x)
                {
                    anim.SetTrigger("Splash Right");
                    cam.SetTrigger("CamShakeRight");

                }
                else
                {
                    anim.SetTrigger("Splash Left");
                    cam.SetTrigger("CamShakeLeft");
                }
            }

            ///// SCORING /////

            score = GameManager.Instance.GetScore();
            memoire = multiplier;


            if (countRebond >= 18 && PlayerHitTheBall == false)
            {
                interfaceScore.SetBool("playScoreMaxed", true);
                otheranim = true;

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
