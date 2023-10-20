using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    //FOLLOWED TUTORIAL FROM: https://www.youtube.com/watch?v=0T5ei9jN63M&ab_channel=CodeMonkey
    public event EventHandler OnHealthChanged;
    private int health;
    public int maxHealth = 100;
    public int damage = 10;
     public HealthBar healthBar;
    private int currentHealth;

    //Set current health of player/enemy to max health at the start
    void Awake()
    {
        currentHealth = maxHealth;
        //healthBar.Setup(HealthSystem);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent() {
        return health / maxHealth;
    }

    //Adds method for lowering player/enemy health and death state
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Die(); //function for when entity dies
        }

        if (OnHealthChanged != null) 
        {
            OnHealthChanged(this, EventArgs.Empty);
        }
        
    }

    //Method for heals from various sources
    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (OnHealthChanged != null) 
        {
            OnHealthChanged(this, EventArgs.Empty); 
        }
       
    }
    

    
}
