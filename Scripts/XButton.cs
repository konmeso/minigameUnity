using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XButton : MonoBehaviour
{
    public void xButtonPressed()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //PlayerPrefs.DeleteKey("latestScore");
        //PlayerPrefs.DeleteKey("latestName");
    }
}
