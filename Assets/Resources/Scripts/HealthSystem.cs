using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem 
{
    //FOLLOWED TUTORIAL FROM: https://www.youtube.com/watch?v=0T5ei9jN63M&ab_channel=CodeMonkey
    public event EventHandler OnHealthChanged;
    private int health;
    private int healthMax;
    public HealthSystem(int healthMax) {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth(){
        return health;
    }

    public float GetHealthPercent() {
        return health / healthMax;
    }

    public void Damage(int damageAmount) {
        health -=damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount) {
        health += healAmount;
        if(health > healthMax) health = healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
