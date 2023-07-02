using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    
    public FightLevelController levelController;
    public TextMeshProUGUI hpText;
    //public Text hpText;
    public int maxHealth = 500;
    public int health;
    public int attack = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        // Full health at start
        health = maxHealth;

        levelController = GameObject.Find("LevelController").GetComponent<FightLevelController>();
        //hpText = GameObject.Find("hpText").GetComponent<TextMeshProUGUI>();

        // Double attack damage (edit as needed)
        if (PowerupsList.GetInstance().hasBonusAtk == true)
        {
            attack *= 2;
            Debug.Log("Player damage doubled!");
        }
    }

    void FixedUpdate()
    {
        if (hpText != null)
        {
            hpText.text = health.ToString();
        }
    }

    // Take damage from enemy/boss
    public void DamageTaken(int damage)
    {
        // If invincibility is true, no health lost
        if (PowerupsList.GetInstance().hasInvincibility == false)
        {
            health -= damage;
            Debug.Log("Player has taken damage! :( Current health: " + health);

            if (health <= 0)
            {
                levelController.LoseLevel();
            }
        }
        else
        {
            Debug.Log("you're invincible!");
        }
    }

    public void KillEnemy(GameObject enemy)
    {
        levelController.KillEnemy(enemy);
    }
}
