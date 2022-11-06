using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class HighscoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private string score_string;
    private int latest_score;
    private string latest_name;

    private void Awake() 
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");        
    
        entryTemplate.gameObject.SetActive(false); //We turn off the current template

        //score_string = System.IO.File.ReadAllText(Application.dataPath + "/StoreFiles/theScore.txt");
        latest_score = ScoreHolder.Instance.score;
        //latest_name = System.IO.File.ReadAllText(Application.dataPath + "/StoreFiles/theName.txt");
        latest_name = ScoreHolder.Instance.playerName;

        AddHighscoreEntry(latest_score, latest_name);

        PlayerPrefs.DeleteKey("latestScore");
        PlayerPrefs.DeleteKey("latestName");

        if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "StoreFiles")))
        {
            Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "StoreFiles"));
        }
        string path = Path.Combine(Application.streamingAssetsPath, "StoreFiles", "highscores.json");


        //string jsonStrign = PlayerPrefs.GetString("highscoreTable");
        string jsonStrign = File.ReadAllText(path);

        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonStrign);


        // Sort entry list by score
        for (int i= 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
                
            }
        }


        int k=0;
        highscoreEntryTransformList = new List<Transform>();


        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if (k < 5)
            { 
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);  
            }
            k++;
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -40 - templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank.ToString();

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {

        if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "StoreFiles")))
        {
            Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "StoreFiles"));
        }
        string path = Path.Combine(Application.streamingAssetsPath, "StoreFiles", "highscores.json");

        // Create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved highscores
        //string jsonStrign = PlayerPrefs.GetString("highscoreTable");
        string jsonStrign = File.ReadAllText(path);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonStrign);

        // Add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);


        // Save updated highscores
        string json = JsonUtility.ToJson(highscores);
        File.WriteAllText(path,json);
        //PlayerPrefs.SetString("highscoreTable", json);
        //PlayerPrefs.Save();
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    // Represents a single highscore entry

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }

}
