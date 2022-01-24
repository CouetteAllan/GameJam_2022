using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float force;
    [SerializeField] float speed;
    [SerializeField] int score;
    Rigidbody2D rb;
    Vector2 speedVector;
    Vector2 dir = new Vector2(1, -1);
    [SerializeField] List<GameObject> wall = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collider2D other)
    {
        for (int i = 0; i < wall.Count; i++)
        {
            wall[i] = GetComponent<GameObject>();
            if (wall[i] != null)
            {
                score += 100;
                speed = force * score;
                speedVector = new Vector2(rb.velocity.x, rb.velocity.y);
                rb.velocity += speedVector * speed;
            }
        }
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {

        }
    }
}
