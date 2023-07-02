using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy
{
    public int health;
    public int attack_damage;
    public float attack_speed;
    public float move_speed;

    public Enemy(int health, int attack_damage, float projectile_speed, float attack_speed, float move_speed)
    {
        this.health = health;
        this.attack_damage = attack_damage;
        this.attack_speed = attack_speed;
        this.move_speed = move_speed;
    }

    ~Enemy()
    {
        Debug.Log("enemy destroyed");
    }
}
