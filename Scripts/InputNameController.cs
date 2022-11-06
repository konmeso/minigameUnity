using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;
using UnityEngine.UI;

public class InputNameController : MonoBehaviour
{
    public GameObject inputField;
    public string theName;
    //private string SavePath2 => $"{Application.streamingAssetsPath}/StoredName.json";

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        //PlayerPrefs.SetString("latestName", theName);
        //System.IO.File.WriteAllText(Application.dataPath + "/StoreFiles/theName.txt", theName.ToString());
        ScoreHolder.Instance.playerName = theName;
    }

    public void PlayGame()
    {
        StoreName();
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
