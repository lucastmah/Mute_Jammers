using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Enemy enemy;
    public int health;
    public int attack_damage;
    public float projectile_speed;
    public float attack_speed;
    public int move_speed;

    void Start()
    {
        enemy = new Enemy(health, attack_damage, projectile_speed, attack_speed, move_speed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
