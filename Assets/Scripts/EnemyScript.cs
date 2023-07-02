using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Monster
{
    Melee = 1,
    Ranged = 2,
    Bomber = 3,
    Mage = 4,
    Boss = 5,
    Floater = 6

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
    public float move_speed;
    public int enemy_type;
    public float attack_timer;
    public bool invincibility;
    public Vector3 direction;
    public Vector3 boss_direction;
    [SerializeField] public GameObject deathParticles;
    //private FightLevelController fightLevelController;

    public SpriteRenderer sprite;

    // mage
    int xDir = 0;
    int yDir = 0;

    void Start()
    {
        enemy = new Enemy(health, attack_damage, projectile_speed, attack_speed, move_speed, invincibility);
        player = GameObject.Find("Player");
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        if (enemy_type == (int)Monster.Boss)
        {
            // Get initial player direction for boss to follow
            if (player.transform.position.x < transform.position.x)
            {
                boss_direction = Vector3.left;
            }
            else
            {
                boss_direction = Vector3.right;
            }

            // Boss movement is separate from other enemies b/c independent of player location
            Invoke("BossMovement", 0.5f);
        }
        //fightLevelController = GameObject.Find("LevelController").GetComponent<FightLevelController>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // determine direction which direction enemy should face relative to player
        //Debug.Log("Playerpos = " + player.transform.position);
        if (enemy_type != (int)Monster.Boss)
        {
            if (player.transform.position.x < transform.position.x)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.right;
            }
        }

        // determine attacking behaviour
        Behaviour();
    }

    private void BossMovement()
    {
        // Boss changes directions every 3-5 seconds
        float movement_delay = Random.Range(3, 6);

        if (direction == Vector3.right)
        {
            direction = Vector3.left;
            sprite.flipX = true;
        }

        else
        {
            direction = Vector3.right;
            sprite.flipX = false;
        }

        // Call movement function again after delay
        Invoke("BossMovement", movement_delay);

        // Modified script below to incorporate random boss pauses (currently unused)

        // // Generate random number from 0-2
        // float random_pause = Random.Range(0, 3);

        // // If 2, then character will pause (33% chance)
        // if (random_pause == 2)
        // {
        //     // 1 second pause if boss stopped
        //     float movement_delay = 1;

        //     // No direction to allow boss to pause
        //     direction = Vector3.zero;

        //     // Call movement function again after delay
        //     Invoke("BossMovement", movement_delay);
        // }

        // else
        // {
        //     // Boss changes directions every 3-5 seconds
        //     float movement_delay = Random.Range(3, 6);

        //     if (direction == Vector3.right)
        //     {
        //         direction = Vector3.left;
        //     }

        //     else
        //     {
        //         direction = Vector3.right;
        //     }

        //     // Call movement function again after delay
        //     Invoke("BossMovement", movement_delay);
        // }
    }

    private void Behaviour()
    {
        //Debug.Log("active");
        if (enemy_type == (int)(Monster.Floater)) {
            move_speed = 0.025f;
            if (attack_timer < 0) {
                xDir = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
                yDir = (int)Mathf.Sign(player.transform.position.y - transform.position.y);

                attack_timer = 5;
            }
            attack_timer--;
            transform.transform.position += new Vector3(xDir, yDir, 0) * move_speed;
            return;
        }

        // update attack timer
        if (attack_timer < enemy.attack_speed)
        {
            //attack_timer++;
            attack_timer += Time.deltaTime;
        }
        // if Monster = boss, shoot 4 projectiles at a certain range
        // if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5 && enemy_type == (int)Monster.Boss)
        // {
        //     if (attack_timer >= enemy.attack_speed)
        //     {
        //         GameObject projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
        //         projectileInstance.transform.Rotate(0, 0, 0);
        //         projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
        //         projectileInstance.transform.Rotate(0, 0, 60);
        //         projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
        //         projectileInstance.transform.Rotate(0, 0, 120);
        //         projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
        //         projectileInstance.transform.Rotate(0, 0, 180);
        //         //Debug.Log("Fire");
        //         attack_timer = 0;
        //     }
        // }
        // if Monster = ranged and close enough, start shooting when possible
        else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2 && enemy_type == (int)Monster.Ranged)
        {
            if (attack_timer >= enemy.attack_speed)
            {
                GameObject projectileInstance = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), transform.rotation);
                projectileInstance.transform.Rotate(0, 0, (direction == Vector3.right) ? 0 : 180);
                //Debug.Log("Fire");
                attack_timer = 0;
            }
        }

        // if mage and close enough, start shooting when possible
        //else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2 && enemy_type == (int)Monster.Mage)
        //{
        //    if (attack_timer >= enemy.attack_speed)
        //    {
        //        mageAttack.StartMageAttack(attack_damage, player);
        //        attack_timer = 0;
        //    }
        //}

        // move towards player
        else
        {
            transform.position = transform.position + enemy.move_speed  * direction * Time.deltaTime;
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
                    //Debug.Log("Melee enemy attacks player!");
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

            else if (enemy_type == (int)Monster.Floater)
            {
                playerStats.DamageTaken(enemy.attack_damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<ProjectileBehavior>().ProjectileClass.damage);
            //Debug.Log(enemy.health);
            if (enemy.health <= 0)
            {
                Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
                //playerStats.WinLevel();
                playerStats.KillEnemy(this.gameObject);
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }

        // Yeet the projectile
        //if (collision.gameObject != null)
          //  Destroy(collision.gameObject);
    }

    private void TakeDamage(int damage)
    {
        enemy.health -= damage;
    }
}
