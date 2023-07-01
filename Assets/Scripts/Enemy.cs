using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy
{
    public int health;
    public Projectile projectile;
    public float attack_speed;
    public float move_speed;


    public Enemy(int health, int attack_damage, float projectile_speed, float attack_speed, float move_speed)
    {
        this.health = health;
        this.projectile = new Projectile(false, attack_damage, projectile_speed);
        this.attack_speed = attack_speed;
        this.move_speed = move_speed;
    }

    ~Enemy()
    {
        Debug.Log("enemy destroyed");
    }
}
