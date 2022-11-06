using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour { 



    public void Highscores()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
