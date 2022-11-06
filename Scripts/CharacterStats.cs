using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float currHealth;
    public float maxHealth;
    public float currStamina;
    public float maxStamina;

    public bool isDead = false;

    public virtual void CheckHealth()
    {
        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }
        if (currHealth <= 0)
        {
            currHealth = 0;
            isDead = true;
            Die();
        }
    }

    public virtual void CheckStamina()
    {
        if (currStamina >= maxStamina)
        {
            currStamina = maxStamina;
        }
        if (currStamina <= 0)
        {
            currStamina = 0;
        }
    }
    // Start is called before the first frame update
    public virtual void Die() 
    {
        // Override.
    }
    

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
    }
}
