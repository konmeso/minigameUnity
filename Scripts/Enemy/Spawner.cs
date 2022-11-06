using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject enemy;
    public GameObject gameController;

    [SerializeField] int numberOfLevels;

    public int waveNumber;
    public int enemySpawnAmount;
    public int enemiesKilled = 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
        spawners = new GameObject[3];
        gameController = GameObject.FindGameObjectWithTag("GameController");

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();

    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled >= enemySpawnAmount)
        {
            NextWave();
        }

        if(waveNumber > numberOfLevels)
        {
            gameController.GetComponent<GameController>().WriteScore();
            StartCoroutine(LoadSceneWithIndex(3));
        }
    }

    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
    }

    private void StartWave()
    {
        waveNumber = 1;
        enemySpawnAmount = 3;
        enemiesKilled = 0;

        for(int i = 0; i < enemySpawnAmount-1; i++)
        {
            SpawnEnemy();
        }

    }

    public void NextWave()
    {
        waveNumber++;
        enemySpawnAmount +=3;
        enemiesKilled = 0;
        gameController.GetComponent<GameController>().minutesofPlayTime++;


        for(int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    IEnumerator LoadSceneWithIndex(int index)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        //disable auto activation
        asyncOp.allowSceneActivation = false;


        //check if loading is done
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                //load the scene
                asyncOp.allowSceneActivation = true;
                yield return null;
            }
            else
            {
                //add text "press any key to continue"
                yield return null;
            }
        }
    }

}
