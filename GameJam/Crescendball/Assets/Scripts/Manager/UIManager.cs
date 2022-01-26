using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Instances;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("UIManager Instance not found.");

            return instance;
        }

    }

    #endregion

    private void OnEnable()
    {
        instance = this;
    }

    public TextMeshProUGUI score_text;
    public TextMeshProUGUI mult_text;

    public GameObject[] LifeIcons;

    public void Wiggle()
    {

    }

    public void PauseMenu(bool active)
    {

    }

    public void UpdateScore(int score)
    {
        score_text.text = score.ToString();
    }
    public void UpdateMult(int mult)
    {
        mult_text.text = "x" + mult.ToString();
    }
    

    public void UpdateLife(int HP)
    {
        for (int i = 0; i < LifeIcons.Length; i++)
        {
            LifeIcons[i].SetActive(false);
        }

        for (int i = 0; i < HP; i++)
        {
            LifeIcons[i].SetActive(true);
        }
    }

}
