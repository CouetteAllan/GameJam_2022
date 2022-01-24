using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool hitBall = false;
    float timerCooldown;
    float hitTimer = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        this.rgbd2D = this.GetComponent<Rigidbody2D>();
        hitZone.SetActive(false);
        GameManager.Instance.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        ReadingInput();
        timerCooldown -= Time.deltaTime;
        hitTimer -= Time.deltaTime;
        if(hitTimer <= 0)
        {
            Hit(false);
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
        if (Input.GetKeyDown(KeyCode.Space) && timerCooldown <= 0)
        {
            Hit(true); //frapper la balle
            timerCooldown = hitBall ? 0.3f : 1.0f;
            hitTimer = 0.3f;
        }

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0 && facingRight)
        {
            Flip();
        }
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }

    }

    private void Hit(bool hit)
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
