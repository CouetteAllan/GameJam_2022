using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{
    Vector2 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            BallScript ball = collision.gameObject.GetComponent<BallScript>();

            ball.GetComponent<Rigidbody2D>().velocity = -ball.GetComponent<Rigidbody2D>().velocity * 3f;
            int score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore( score += 200);
        }
    }
}
