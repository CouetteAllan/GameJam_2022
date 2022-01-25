using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{

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
            float stopDuration = 0.8f;
            GameManager.Instance.Stop(stopDuration);
            arrowSprite.enabled = true;

            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            StartCoroutine(AimingDir(stopDuration,ball));
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
        ball.countRebond = 0;
        int totalScore = 100 * ((int)ball.multiplier * 2);
        GameManager.Instance.SetScore(GameManager.Instance.GetScore() + totalScore);
        PopUpScore.Create(GameManager.Instance.GetPlayer().transform.position + Vector3.up * 2, totalScore, (int)ball.multiplier);

    }

}
