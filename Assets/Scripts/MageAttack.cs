using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    private int attack_damage;
    public GameObject projectile;

    // // spawns explosive on player
    // IEnumerator SpawnOrb(GameObject player)
    // {
    //     GameObject projectileInstance = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y), player.transform.rotation);
    //     Debug.Log("Mage orb spawned!");

    //     float time_elapsed = 3;
    //     yield return new WaitForSeconds(time_elapsed);
    //     projectileInstance.GetComponent<BoxCollider2D>().enabled = true;

    //     yield return null;
    //     Destroy(projectileInstance);
    // }

    public void StartMageAttack(int attack_damage, GameObject player)
    {
        this.attack_damage = attack_damage;
        GameObject projectileInstance = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y), player.transform.rotation);
        // StartCoroutine(SpawnOrb(player));
    }
}
