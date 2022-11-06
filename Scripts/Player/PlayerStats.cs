using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{

    PlayerUI playerUI;
    GameController gameController;
    public bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerUI = GetComponent<PlayerUI>();

        maxHealth = 100;
        currHealth = maxHealth;

        maxStamina = 100;
        currStamina = maxStamina;

        SetStats();
    }
    
   

    // Update is called once per frame
    
    void SetStats()
    {
        playerUI.healthAmount.text = currHealth.ToString();
        playerUI.staminaAmount.text = currStamina.ToString("0");
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        SetStats();
    }

    public override void CheckStamina()
    {
        base.CheckStamina();
        SetStats();
    }

    public override void Die()
    {
        //Debug.Log("You died!");
        gameController.GetComponent<GameController>().WriteScore();
        //SceneManager.LoadScene(3, LoadSceneMode.Single);
        StartCoroutine(LoadSceneWithIndex(3));

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
