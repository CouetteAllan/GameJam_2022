using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    private ParticleSystem particles;


    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();
        if(ball.multiplier >= 10)
        {
            particles.Play();
            PopUpScore.Create(this.transform.position += Vector3.up * 1.2f, 400 * (int)ball.multiplier);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    
}
