using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{

    bool hit;
    private PlayerScript player;
    private Vector2 lastGoodDirection = new Vector2(1, 1);
    private float timer = 0.4f;

    private void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(player == null)
        {
            player = GameManager.Instance.GetPlayer();
        }

        Vector2 direction = player.action.Player.Movement.ReadValue<Vector2>().normalized;
        Debug.Log(direction);

        if (direction.x > 0 || direction.y > 0)
        {
            lastGoodDirection = direction;

        }
        timer -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            hit = true;
            GameManager.Instance.Stop(0.4f);


            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            float velocityMagnitude = ball.GetComponent<Rigidbody2D>().velocity.magnitude;
            Vector2 dir = lastGoodDirection;
            var magnitude = ball.GetComponent<Rigidbody2D>().velocity.magnitude;
            ball.GetComponent<Rigidbody2D>().velocity = dir * magnitude * 2;
            int score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore( score += 200);
            player.HitBall = true;
        }
    }

    private Vector2 Direction()
    {
        Vector2 direction = player.action.Player.Movement.ReadValue<Vector2>().normalized;

        return direction;
    }
}
