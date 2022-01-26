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
        if (text != null)
        {
            Debug.Log("Mult_TXT non NULL");
            if (ball.multiplier == 0)
            {
                text.fontSize = 36;
                textColor = Color.white;
            }
            else if (ball.multiplier == 1)
            {
                text.fontSize = 40;
                textColor = new Color(245.0f / 255.0f, 255.0f / 255.0f, 0 / 255.0f);
            }

            else if (ball.multiplier == 2)
            {
                text.fontSize = 44;
                textColor = new Color(75.0f / 255.0f, 255.0f / 255.0f, 0 / 255.0f);

            }

            else if (ball.multiplier == 3)
            {
                text.fontSize = 48;
                textColor = new Color(0.0f / 255.0f, 132.0f / 255.0f, 21.0f / 255.0f);

            }

            else if (ball.multiplier == 4)
            {
                text.fontSize = 52;
                textColor = new Color(0.0f / 255.0f, 154.0f / 255.0f, 139.0f / 255.0f);

            }

            else if (ball.multiplier == 5)
            {
                text.fontSize = 56;
                textColor = new Color(0.0f / 255.0f, 31.0f / 255.0f, 255.0f / 255.0f);

            }

            else if (ball.multiplier == 6)
            {
                text.fontSize = 60;
                textColor = new Color(101.0f / 255.0f, 58.0f / 255.0f, 241.0f / 255.0f);

            }

            else if (ball.multiplier == 7)
            {
                text.fontSize = 64;
                textColor = new Color(104.0f / 255.0f, 0.0f / 255.0f, 229.0f / 255.0f);

            }

            else if (ball.multiplier == 8)
            {
                text.fontSize = 68;
                textColor = new Color(226.0f / 255.0f, 33.0f / 255.0f, 199.0f / 255.0f);

            }

            else if (ball.multiplier == 9)
            {
                text.fontSize = 72;
                textColor = new Color(212.0f / 255.0f, 43.0f / 255.0f, 53.0f / 255.0f);
            }
            else
            {
                text.fontSize = 80;
                textColor = Color.red;
            }
            text.color = textColor;
        }
    }
}
