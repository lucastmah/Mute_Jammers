using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public FightLevelController levelController;
    public int maxHealth = 500;
    public int health;
    public int attack = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        // Full health at start
        health = maxHealth;

        levelController = GameObject.Find("LevelController").GetComponent<FightLevelController>();

        // Double attack damage (edit as needed)
        if (PowerupsList.GetInstance().hasBonusAtk == true)
        {
            attack *= 2;
            Debug.Log("Player damage doubled!");
        }
    }

    void FixedUpdate()
    {

    }

    // Take damage from enemy/boss
    public void DamageTaken(int damage)
    {
        // If invincibility is true, no health lost
        if (PowerupsList.GetInstance().hasInvincibility == false)
        {
            health -= damage;
            Debug.Log("Player has taken damage! :(");

            if (health <= 0)
            {
                levelController.OnPlayerDeath();
            }
        }
        else
        {
            Debug.Log("you're invincible!");
        }
    }

    public void LoseGame()
    {
        levelController.OnEnemyDeath();
    }
}
