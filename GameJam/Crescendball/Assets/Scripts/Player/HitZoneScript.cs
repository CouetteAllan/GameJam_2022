using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{

    bool hit;
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
            hit = true;
            BallScript ball = collision.gameObject.GetComponent<BallScript>();

            ball.GetComponent<Rigidbody2D>().velocity = -ball.GetComponent<Rigidbody2D>().velocity * 3f;
            int score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore( score += 200);
            GameManager.Instance.GetPlayer().HitBall = true;
            GameManager.Instance.Stop(0.4f);
        }
    }

    private Vector2 Direction()
    {
        PlayerScript player = GameManager.Instance.GetPlayer();
        Vector2 pos = player.transform.position;
        Vector2 direction = player.transform.right;



        return direction;
    }
}
