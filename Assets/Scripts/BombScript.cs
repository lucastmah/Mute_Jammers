using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameObject player;
    public int attack_damage = 40;
    private float timer = 0;
    private float attack_delay = 2;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (timer < attack_delay)
        {
            timer += Time.deltaTime;
        }

        else
        {
            ExplodeBomb();
        }
    }

    public void ExplodeBomb()
    {
        if (this.GetComponent<BoxCollider2D>().bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
        {
            // Placeholder value for mage bomb damage
            player.GetComponent<PlayerController>().Hurt(attack_damage);
            Debug.Log("Mage orb exploded for " + attack_damage + " damage!");
            Destroy(this.gameObject);
        }
    }
}
