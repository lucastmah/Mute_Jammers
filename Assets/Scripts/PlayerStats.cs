using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PowerupsList powerups;
    public FightLevelController levelController;
    public int maxHealth = 500;
    public int health;
    public int attack = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        // Full health at start
        health = maxHealth;

        // Double health (edit as needed)
        if (powerups.hasBonusMaxHp == true)
        {
            health *= 2;
            Debug.Log("Player max health doubled!");
        }

        // Double attack damage (edit as needed)
        if (powerups.hasBonusAtk == true)
        {
            attack *= 2;
            Debug.Log("Player damage doubled!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Passively regen HP after each frame if not full
        if (powerups.hasRegen == true)
        {
            if (health < maxHealth)
            {
                health += 1;
                Debug.Log("Player has regenerated 1 HP!");
            }
        }
    }

    // Take damage from enemy/boss
    public void DamageTaken(int damage)
    {
        // If invincibility is true, no health lost
        if (powerups.hasInvincibility == false)
        {
            health -= damage;
            Debug.Log("Player has taken damage! :(");

            if (health <= 0)
            {
                levelController.OnPlayerDeath();
            }
        }
    }
}
