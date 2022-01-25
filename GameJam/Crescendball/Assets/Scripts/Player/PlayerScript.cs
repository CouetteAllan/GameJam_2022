using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 25;
    bool facingRight = true;


    private int hp;
    public int Hp { get => hp; set => hp = value; }

    private float vertical;
    private float horizontal;

    private Rigidbody2D rgbd2D;
    public GameObject hitZone;
    public PlayerInputAction action;

    bool hitBall = false;
    float hitTimer = 0.3f;
    public bool HitBall {
        get => hitBall;
        set {
            hitBall = value;
            if (HitBall)
            {
                hitTimer = 0;
            }
        }
    }

    float timerCooldown;
    float invincibleTimer = 1.8f;

    // Start is called before the first frame update
    void Awake()
    {
        hp = 3;
        this.rgbd2D = this.GetComponent<Rigidbody2D>();
        

        action = new PlayerInputAction();
        action.Player.Enable();
        action.Player.Shoot.performed += Shoot_performed;
        action.Player.Movement.performed += Movement_performed;
    }
    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
        hitZone.SetActive(false);

    }
    void Update()
    {
        ReadingInput();
        timerCooldown -= Time.deltaTime;
        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0)
        {
            Shoot(false);
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(horizontal, vertical);
        rgbd2D.AddForce(dir * speed ,ForceMode2D.Force);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
        if (!GameManager.Instance.waiting)
        {

            if (horizontal < 0 && facingRight)
            {
                Flip();
            }
            if (horizontal > 0 && !facingRight)
            {
                Flip();
            }
        }

    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if(timerCooldown <= 0)
        {
            Shoot(true); //frapper la balle
            timerCooldown = hitBall ? 0.3f : 1.0f;
            hitTimer = 0.2f;
        }
    }

    

    private void ReadingInput()
    {

        horizontal = action.Player.Movement.ReadValue<Vector2>().x;
        vertical = action.Player.Movement.ReadValue<Vector2>().y;
    }

    private void Shoot(bool hit)
    {
        hitZone.SetActive(hit);

    }



    private void Flip() //retourne le sprite
    {

        facingRight = !facingRight;


        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GameManager.Instance.Stop(0.2f, 0.1f);
        }
    }
}
