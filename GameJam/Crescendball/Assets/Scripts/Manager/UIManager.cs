using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    public EventSystem eventSystem;
    public GameObject pauseMenuObj;
    public GameObject pauseMenuFirstSelectable;
    public GameObject gameOverPanelObj;
    public GameObject gameOverFirstSelectable;
    public GameObject[] LifeIcons;



    private void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void PauseMenu(bool active)
    {
        pauseMenuObj.SetActive(active);
        eventSystem.firstSelectedGameObject = pauseMenuFirstSelectable;
    }

    public void GameOverPanel(bool active)
    {
        gameOverPanelObj.SetActive(active);
        eventSystem.firstSelectedGameObject = gameOverFirstSelectable;

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
