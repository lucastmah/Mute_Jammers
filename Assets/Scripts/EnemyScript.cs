using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Monster
{
    Melee = 1,
    Ranged = 2,
    Bomber = 3
}

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    public GameObject projectile;
    private Enemy enemy;
    public int health;
    public int attack_damage;
    public float projectile_speed;
    public float attack_speed;
    public int move_speed;
    public int enemy_type;
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
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1 && enemy_type == (int)Monster.Melee)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                // deal damage to player
                attack_timer = 0;
            }
        }
        // if ranged and close enough, start shooting when possible
        else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2 && enemy_type == (int)Monster.Ranged)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                GameObject projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
                int projDirection = (direction == Vector3.right) ? 1 : -1;
                projectileInstance.transform.localScale = new Vector3(projDirection, 1, 1);
                //Debug.Log("Fire");
                attack_timer = 0;
            }
        }
        // move towards player
        else
        {
            transform.position = transform.position + enemy.move_speed * Time.deltaTime * direction;
            Debug.Log("move");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<ProjectileBehavior>().ProjectileClass.damage);
            Debug.Log(enemy.health);
            if (enemy.health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (enemy_type == (int)Monster.Bomber)
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
