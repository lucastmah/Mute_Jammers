using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool invincibility;

    public Boss(int health, int attack_damage, float projectile_speed, float attack_speed, float move_speed):base(health, attack_damage, projectile_speed, attack_speed, move_speed)
    {   
        this.invincibility = true;
    }

    ~Boss()
    {
        Debug.Log("Boss destroyed");
    }
}
