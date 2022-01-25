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
        mult_text.text = mult.ToString();
    }
    
}
