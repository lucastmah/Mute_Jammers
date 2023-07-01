using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // public int health;
    // public int attack_damage;
    // public float move_speed;
    public bool invincibility;

    public Boss(int health, int attack_damage, float move_speed)
    {
        this.health = health;
        this.attack_damage = attack_damage;
        this.move_speed = move_speed;
        this.invincibility = true;
    }

    ~Boss()
    {
        Debug.Log("Boss destroyed");
    }
}
