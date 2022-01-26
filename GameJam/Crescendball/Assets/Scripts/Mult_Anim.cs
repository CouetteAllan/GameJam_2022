using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mult_Anim : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color textColor;

    [SerializeField] BallScript ball;
    // Start is called before the first frame update
    private void Awake()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.multiplier == 0)
        {
            text.fontSize = 36;
            textColor = Color.white;
        }
        else if (ball.multiplier < 1)
        {
            text.fontSize = 40;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);
        }

        else if (ball.multiplier < 2)
        {
            text.fontSize = 44;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 3)
        {
            text.fontSize = 48;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 4)
        {
            text.fontSize = 52;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 5)
        {
            text.fontSize = 56;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 6)
        {
            text.fontSize = 60;
            textColor = new Color(255.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 7)
        {
            text.fontSize = 64;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 8)
        {
            text.fontSize = 68;
            textColor = new Color(245.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f);

        }

        else if (ball.multiplier < 9)
        {
            text.fontSize = 72;
            textColor = new Color(255.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f);
        }
        else
        {
            text.fontSize = 80;
            textColor = Color.red;
        }
    }
}
