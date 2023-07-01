using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public class Enemy
    {
        public float health;
        public float attack_damage;
        public float move_speed;

        public Enemy(float health, float attack_damage, float move_speed)
        {
            this.health = health;
            this.attack_damage = attack_damage;
            this.move_speed = move_speed;
        }

        ~Enemy()
        {
            Debug.Log("enemy destroyed");
        }
    }
}
