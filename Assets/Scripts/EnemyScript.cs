using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Monster
{
    Melee = 1,
    Ranged = 2,
    Bomber = 3,
    Mage = 4,
    Boss = 5
}

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    public GameObject projectile;
    public MageAttack mageAttack;
    private Enemy enemy;
    public int health;
    public int attack_damage;
    public float projectile_speed;
    public float attack_speed;
    public int move_speed;
    public int enemy_type;
    public float attack_timer;
    public bool invincibility;
    public Vector3 direction;

    void Start()
    {
        enemy = new Enemy(health, attack_damage, projectile_speed, attack_speed, move_speed, invincibility);
    }
    // Update is called once per frame
    void Update()
    {
        // determine direction which direction enemy should face relative to player
        //Debug.Log("Playerpos = " + player.transform.position);
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
        // if ranged and close enough, start shooting when possible
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2 && enemy_type == (int)Monster.Ranged)
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

        // if mage and close enough, start shooting when possible
        else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2 && enemy_type == (int)Monster.Mage)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                mageAttack.StartMageAttack(attack_damage, player);
                attack_timer = 0;
            }
        }

        // move towards player
        else
        {
            transform.position = transform.position + enemy.move_speed * Time.deltaTime * direction;
            //Debug.Log("move");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision detected");    
        //Debug.Log("enemy has hit " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enemy_type == (int)Monster.Melee)
            {
                if (attack_timer >= attack_speed)
                {
                    playerStats.DamageTaken(enemy.attack_damage);
                }
            }

            else if (enemy_type == (int)Monster.Bomber)
            {
                //Debug.Log("you've been bombed");
                Debug.Log("BOOM!");
                playerStats.DamageTaken(enemy.attack_damage);
                Destroy(gameObject);
            }
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
    }

    private void TakeDamage(int damage)
    {
        enemy.health -= damage;
    }
}
