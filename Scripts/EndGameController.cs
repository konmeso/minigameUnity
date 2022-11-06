using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI totalScore;

    private string nameofUser;
    private int scoreofUser;

    void Start()
    {
        Time.timeScale = 1;
        UpdateEndGameUI();    
    }

    public void ReturnHome()
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void UpdateEndGameUI()
    {
        //nameofUser = System.IO.File.ReadAllText(Application.dataPath+ "/StoreFiles/theName.txt");
        //scoreofUser = System.IO.File.ReadAllText(Application.dataPath+"/StoreFiles/theScore.txt");

        nameofUser = ScoreHolder.Instance.playerName;
        scoreofUser = ScoreHolder.Instance.score;


        totalScore.text = scoreofUser.ToString();
        playerName.text = nameofUser;
    }
}
