using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpScore : MonoBehaviour
{
    private TextMeshPro text;
    private Transform textTransform;
    private float timerMax = 0.6f;
    private float timer;
    private Vector2 moveDir;
    private bool player = false;

    private Color textColor;
    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }


    public static PopUpScore Create(Vector2 pos,int scoreAmount, int multiplier = 20, bool jackpot = false)
    {
        Transform scoreTransform = Instantiate(GameManager.Instance.scorePopUp,pos,Quaternion.identity);
        PopUpScore score = scoreTransform.GetComponent<PopUpScore>();
        score.Setup(scoreAmount,multiplier,jackpot);
        return score;
    }
    public void Setup(int scoreAmount, int multiplier = 20, bool jackpot = false)
    {
        text.SetText(scoreAmount.ToString());
        if(multiplier < 3)
        {
            text.fontSize = 3;  
            textColor = Color.white;
        }
        else if (multiplier < 6)
        {
            text.fontSize = 5;
            textColor = new Color(255.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f); 

        }
        else if(multiplier < 9)
        {
            text.fontSize = 7;
            textColor = new Color(255.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f);

        }
        else
        {
            text.fontSize = 9.5f;
            textColor = Color.red;
            if (jackpot)
            {
                player = jackpot;
                text.fontSize = 12;
                timerMax = 1.2f;
                timer = 1.2f;
            }
        }

        text.color = textColor;
        moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 10.0f;
    }

    void Start()
    {
        timer = timerMax;
    }
    void Update()
    {
        transform.position += (Vector3)moveDir * Time.deltaTime;
        moveDir -= moveDir * 8 * Time.deltaTime;

        if(timer > timerMax * .5f)
        {
            float increaseScaleAmount = player ? 0.6f : 1.1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = player ? 0.6f : 1.1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        if (player)
        {
            textColor += new Color(((-255 / 255) / timerMax) * Time.deltaTime, ((255 / 255) / timerMax) * Time.deltaTime, 0,0);
            text.color = textColor;
        }

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            float fadeAmount = 3f;
            textColor.a -= fadeAmount * Time.deltaTime;
            text.color = textColor;
            if(text.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
