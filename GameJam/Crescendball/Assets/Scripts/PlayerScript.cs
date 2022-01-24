using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 25;

    private int hp;
    public int Hp { get => hp; set => hp = value; }

    private float vertical;
    private float horizontal;

    private Rigidbody2D rgbd2D;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        this.rgbd2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadingInput();
    }

    private void FixedUpdate()
    {
        Vector2 pos = rgbd2D.position;
        pos += new Vector2(horizontal, vertical) * speed / 60.0f;
        rgbd2D.position = pos;
    }

    private void ReadingInput()
    {
        //Hit(); //frapper la balle
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");


    }

    private void Hit()
    {
        throw new NotImplementedException();
    }
}
