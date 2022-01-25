using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnScore : MonoBehaviour
{


    public GameObject scoreObject;

    public BallScript ball;
    void Start()
    {
        Instantiate(scoreObject,(Vector3)ball.lastGoodVel,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject scoreObj = Instantiate(scoreObject, (Vector3)ball.lastGoodVel, Quaternion.identity);

    }
}
