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
    private PlayerInput playerInput;

    bool hitBall = false;
    public bool HitBall {
        get => hitBall;
        set {
            hitBall = value;
        }
    }

    float timerCooldown;
    float hitTimer = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        hp = 3;
        this.rgbd2D = this.GetComponent<Rigidbody2D>();
        playerInput = this.GetComponent<PlayerInput>();
        

        PlayerInputAction action = new PlayerInputAction();
        action.Player.Enable();
        action.Player.Shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if(timerCooldown <= 0)
        {
            Shoot(true); //frapper la balle
            timerCooldown = hitBall ? 0.3f : 1.0f;
            hitTimer = 0.3f;
        }
    }

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
        hitZone.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ReadingInput();
        timerCooldown -= Time.deltaTime;
        hitTimer -= Time.deltaTime;
        if(hitTimer <= 0)
        {
            Shoot(false);
        }




    }

    private void FixedUpdate()
    {
        Vector2 pos = rgbd2D.position;
        pos += new Vector2(horizontal, vertical) * speed / 60.0f;
        rgbd2D.MovePosition(pos);
    }

    private void ReadingInput()
    {

        /*vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0 && facingRight)
        {
            Flip();
        }
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        */
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
}
