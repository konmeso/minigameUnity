using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    Spawner spawn;

    public int currScore = 0;
    [SerializeField] Text scoreAmount;

    public float minutesofPlayTime;
    [SerializeField] Text minutesLeft;
    [SerializeField] Text secondsLeft;
    [SerializeField] Text level;


    float minutesLeft_n;
    float secondsLeft_n;
    float last_meas;

    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
        UpdateScoreUI();

        minutesLeft_n = minutesofPlayTime;
        secondsLeft_n = 00;
        updateTimeUI();
        updateLevelUI();
    }

    void Update() 
    {
        if (minutesofPlayTime * 60 - Time.time >= 0)
        {
            minutesLeft_n = Mathf.FloorToInt((minutesofPlayTime * 60 - Time.time)/60);
            secondsLeft_n = Mathf.FloorToInt((minutesofPlayTime * 60 - Time.time) % 60);
        }
        else 
        {
            minutesLeft_n = 0;
            secondsLeft_n = 0;
            WriteScore();
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        
        updateTimeUI(); 
        updateLevelUI();  

        if(Time.time - last_meas > 1)
        {
            last_meas = Time.time;
            TimeScore();
        }

    }


    public void TimeScore()
    {
        currScore += 1;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreAmount.text = currScore.ToString("0");
    }

    private void updateTimeUI()
    {
        minutesLeft.text = minutesLeft_n.ToString();
        secondsLeft.text = secondsLeft_n.ToString();
    }

    public void updateLevelUI()
    {
        spawn = GetComponentInChildren<Spawner>();
        level.text = spawn.waveNumber.ToString();

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(3,LoadSceneMode.Single);
    }

    public void WriteScore()
    {
        //System.IO.File.WriteAllText("Assets/Temp Storage/theScore.txt", currScore.ToString());
        //PlayerPrefs.SetInt("latestScore", currScore);
        ScoreHolder.Instance.score = currScore;
    }


}
