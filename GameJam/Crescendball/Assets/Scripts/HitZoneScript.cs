using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            BallScript ball = collision.gameObject.GetComponent<BallScript>();

            ball.GetComponent<Rigidbody2D>().velocity = -ball.GetComponent<Rigidbody2D>().velocity * 1.05f;
        }
    }
}
