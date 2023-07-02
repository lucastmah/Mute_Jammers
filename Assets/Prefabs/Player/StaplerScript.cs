using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaplerScript : MonoBehaviour
{
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite close;
    [SerializeField] private SpriteRenderer myRenderer;
    [SerializeField] private AudioSource staplerSound;
    [SerializeField] private GameObject projectile;

    [SerializeField] private int openTime = 20; // time for the sprite to stay open in frames
    private int openTimer;
    private float shakeAmount = .5f;
    private float myShake;
    private Vector3 defaultTransform;
    private Vector3 offsetVector;
    int floatStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        openTimer--;
        floatStep++;

        if (openTimer < 0) {
            myRenderer.sprite = close;
        } else {
            myRenderer.sprite = open;
        }
        myShake *= .65f;

        Vector3 floatVector = new Vector3(0, Mathf.Sin(floatStep/24f) * .175f,0);

        transform.position = floatVector + transform.parent.gameObject.transform.position + offsetVector + new Vector3(UnityEngine.Random.Range(-myShake, myShake), UnityEngine.Random.Range(-myShake, myShake), 0);
        
    }

    public void FireStaple(int angle, int dmg) {
        openTimer = openTime;
        myShake = shakeAmount;
        staplerSound.Play();

        // Offset the staple spawn position randomly for a bit of visual *flair*
        float stapleSpawnRange = .1f;
        Vector3 stapleOffset = new Vector3(UnityEngine.Random.Range(-stapleSpawnRange,stapleSpawnRange),UnityEngine.Random.Range(-stapleSpawnRange,stapleSpawnRange),0);

        GameObject t = Instantiate(projectile, stapleOffset + new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
        t.transform.localScale = new Vector3(angle, 1, 1);
        t.GetComponent<ProjectileBehavior>().ProjectileClass.damage = dmg;
    }

    public void VisualUpdate(bool isFacingRight) {
        myRenderer.flipX = !isFacingRight;

        // This vector controls how far away the stapler is from the player's face
        offsetVector = new Vector3(.35f, -.15f, -1);
        offsetVector.x *= (isFacingRight) ? 1 : -1;
    }
}
