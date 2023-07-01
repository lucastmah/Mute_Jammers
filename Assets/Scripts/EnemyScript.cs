using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private Enemy enemy;
    public int health;
    public int attack_damage;
    public float projectile_speed;
    public float attack_speed;
    public int move_speed;
    public int enemy_type; // 1: melee | 2: ranged | 3: creepbruh
    public float attack_timer;
    public Vector3 direction;

    void Start()
    {
        enemy = new Enemy(health, attack_damage, projectile_speed, attack_speed, move_speed);
    }
    // Update is called once per frame
    void Update()
    {
        // determine direction which direction enemy should face relative to player
        if (player.transform.position.x < transform.position.x)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
        // determine attacking behaviour
        Behaviour();
    }

    private void Behaviour()
    {
        // update attack timer
        if (attack_timer < enemy.attack_speed)
        {
            attack_timer += Time.deltaTime;
        }
        // if melee and close enough, hit player
        if (player.transform.position.x - transform.position.x < 1 && enemy_type == 1)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                // deal damage to player
            }
        }
        // if ranged and close enough, start shooting when possible
        else if (player.transform.position.x - transform.position.x < 5 && enemy_type == 2)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }
        }
        // move towards player
        else
        {
            transform.position = transform.position + enemy.move_speed * Time.deltaTime * direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy_type == 3)
        {
            // deal damage to player
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        enemy.health -= damage;
    }
}
