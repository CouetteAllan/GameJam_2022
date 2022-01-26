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
            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            float stopDuration = 0.8f;
            if (ball.multiplier >= 10)
            {
                AudioManager.instance.Play("Homerun");
                stopDuration += 0.4f;
            }
            else
            {
                AudioManager.instance.Play("BatHit");
            }
            GameManager.Instance.Stop(stopDuration);
            arrowSprite.enabled = true;

            StartCoroutine(AimingDir(stopDuration,ball));
            
            player.HitBall = true;
            ball.PlayerHitTheBall = true;
        }
    }

    IEnumerator AimingDir(float duration,BallScript ball)
    {
        var magnitude = ball.GetComponent<Rigidbody2D>().velocity.magnitude;

        yield return new WaitForSecondsRealtime(duration);
        Vector2 dir = player.LastGoodDirection;
        if(dir.x != 0 && dir.y != 0)
        {
            ball.countRebond = 0;
            ball.multiplierDuo = 1;
            int totalScore = 100 * ((int)ball.multiplier * 2);
            GameManager.Instance.SetScore(GameManager.Instance.GetScore() + totalScore);
            PopUpScore.Create(GameManager.Instance.GetPlayer().transform.position + Vector3.up * 1.2f, totalScore, (int)ball.multiplier);
        }
        
        ball.GetComponent<Rigidbody2D>().velocity = dir * magnitude * 1.25f;
        arrowSprite.enabled = false;
        

    }

}
