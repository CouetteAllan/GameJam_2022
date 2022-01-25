using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{

    bool hit;
    private PlayerScript player;
    private Vector2 lastGoodDirection = new Vector2(1, 1);
    private float timer = 0.4f;
    public GameObject arrow;
    private SpriteRenderer arrowSprite;

    private void Awake()
    {
        arrow.transform.position = this.transform.position;
    }
    void Start()
    {

        arrowSprite = arrow.GetComponentInChildren<SpriteRenderer>();
        arrowSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(player == null)
        {
            player = GameManager.Instance.GetPlayer();
        }

        timer -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            hit = true;
            GameManager.Instance.Stop(0.6f);
            arrowSprite.enabled = true;

            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            StartCoroutine(AimingDir(0.6f,ball));
            int score = GameManager.Instance.GetScore();
            GameManager.Instance.SetScore( score += 200);
            player.HitBall = true;
            ball.PlayerHitTheBall = true;
        }
    }

    IEnumerator AimingDir(float duration,BallScript ball)
    {
        var magnitude = ball.GetComponent<Rigidbody2D>().velocity.magnitude;

        yield return new WaitForSecondsRealtime(duration);
        Vector2 dir = player.LastGoodDirection;
        ball.GetComponent<Rigidbody2D>().velocity = dir * magnitude * 1.25f;
        arrowSprite.enabled = false;

    }

}
