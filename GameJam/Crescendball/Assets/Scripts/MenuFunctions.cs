using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.CurrentGameStates = GameManager.GameStates.InGame;

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        GameManager.Instance.CurrentGameStates = GameManager.GameStates.InGame;
    }
}
