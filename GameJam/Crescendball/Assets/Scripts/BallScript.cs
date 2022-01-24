using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] GameObject ball;
    public float force;
    public float speed;
    int score;
    Rigidbody2D rb;
    Vector2 direction;
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

    void OnCollisionEnter2D(Collision2D other)
    {      
        if ( other.gameObject.layer == 6)
        {
            Transform transform = other.gameObject.GetComponent<Transform>();
            if (transform.localScale.x > transform.localScale.y)
            {

            }
            score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore(score += 100);
            //speed = force * score;
            direction = rb.velocity.normalized;
            //rb.velocity *= direction * speed / 100;
            Debug.Log("bite");
        }

        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {

        }
    }
}
