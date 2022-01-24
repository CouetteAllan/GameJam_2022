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


    public int score;


    private PlayerScript player;

    public void SetPlayer(PlayerScript obj)
    {
        player = obj;
    }

    public int GetScore()
    {
        return this.score;
    }
    
    public void SetScore(int score)
    {
        this.score = score;
    }

}
