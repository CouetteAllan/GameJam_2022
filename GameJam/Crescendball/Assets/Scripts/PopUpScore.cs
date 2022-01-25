using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpScore : MonoBehaviour
{
    private TextMeshPro text;
    private Transform textTransform;
    private const float timerMax = 0.7f;
    private float timer;
    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }


    public static PopUpScore Create(Vector2 pos,int scoreAmount)
    {
        Transform scoreTransform = Instantiate(GameManager.Instance.scorePopUp,pos,Quaternion.identity);
        PopUpScore score = scoreTransform.GetComponent<PopUpScore>();
        score.Setup(scoreAmount);
        return score;
    }
    public void Setup(int scoreAmount)
    {
        text.SetText(scoreAmount.ToString());
    }

    void Start()
    {
        timer = timerMax;
    }
    void Update()
    {

        transform.position += Vector3.up * 2.0f * Time.deltaTime;

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
