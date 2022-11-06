using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    [SerializeField] int scoreAddAmount = 10;

    GameController gameController;
    Spawner spawn;

    private void Start() 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spawn = gameController.GetComponentInChildren<Spawner>();
        maxHealth = 100;
        currHealth = maxHealth;

        maxStamina = 100;
        currStamina = maxStamina;
    }

    private void Update() 
    {
        CheckHealth();
    }

   public override void Die() 
   {
        gameController.AddScore(scoreAddAmount);
       
        spawn.enemiesKilled++;
        
        Destroy(gameObject);

   }
}
