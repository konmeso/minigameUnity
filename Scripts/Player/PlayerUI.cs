using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    public Text healthAmount, staminaAmount;
    GameObject healthBar;
    GameObject staminaBar;

    CharacterStats playerStats;

    void Start() 
    {
        playerStats = GetComponent<CharacterStats>();

        SetStats();    
    }

    void Update()
    {
        SetStats();
    }
    void SetStats()
    {
        healthAmount.text = playerStats.currHealth.ToString();
        staminaAmount.text = playerStats.currStamina.ToString("0");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        float healthPercentage = playerStats.currHealth / playerStats.maxHealth;
        healthBar.GetComponent<RectTransform>().localScale = new Vector3 (healthPercentage, 1, 1);

        staminaBar = GameObject.FindGameObjectWithTag("StaminaBar");
        float staminaPercentage = playerStats.currStamina / playerStats.maxStamina;
        staminaBar.GetComponent<RectTransform>().localScale = new Vector3(staminaPercentage, 1, 1);
    }
}
