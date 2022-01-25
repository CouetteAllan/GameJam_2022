using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Instances;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("GameManager Instance not found.");

            return instance;
        }

    }


    #endregion

    private void OnEnable()
    {
        instance = this;
    }

    public enum GameStates
    {
        MainMenu,
        InGame,
        Pause,
        GameOver,
        Win,
    }
    private GameStates currentGameState;
    public GameStates currentGameStates 
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            switch (currentGameState)
            {
                case GameStates.MainMenu:
                    break;


            }


        }
    }

    public bool waiting = false;

    public int score;
    public int mult;

    public Transform scorePopUp;
    private PlayerScript player;
    public void SetPlayer(PlayerScript obj)
    {
        player = obj;
    }

    public PlayerScript GetPlayer()
    {
        return player;
    }

     

    public int GetScore()
    {
        return this.score;
    }
    
    public void SetScore(int score)
    {
        this.score = score;
    }


    public int GetMult()
    {
        return this.mult;
    }

    public void SetMult(int mult)
    {
        this.mult = mult;
    }


    public void Stop(float duration, float timeScale)
    {
        if (waiting)
            return;
        Time.timeScale = timeScale;
        StartCoroutine(Wait(duration));
    }
    public void Stop(float duration)
    {
        Stop(duration, 0.0f);
    }
    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
