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

        if (openTimer < 0) {
            myRenderer.sprite = close;
        } else {
            myRenderer.sprite = open;
        }
        myShake *= .65f;

        transform.position = transform.parent.gameObject.transform.position + offsetVector + new Vector3(UnityEngine.Random.Range(-myShake, myShake), UnityEngine.Random.Range(-myShake, myShake), 0);

    }

    public void FireStaple(int angle) {
        openTimer = openTime;
        myShake = shakeAmount;
        staplerSound.Play();
        GameObject t = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
        t.transform.localScale = new Vector3(angle, 1, 1);
    }

    public void VisualUpdate(bool isFacingRight) {
        myRenderer.flipX = !isFacingRight;

        offsetVector = new Vector3(.25f, 0, -1);
        offsetVector.x *= (isFacingRight) ? 1 : -1;
    }
}
