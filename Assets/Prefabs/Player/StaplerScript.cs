using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
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

        transform.position = transform.parent.gameObject.transform.position + new Vector3(.15f,0,-1) + new Vector3(Random.Range(-myShake, myShake), Random.Range(-myShake, myShake), 0);

    }

    public void FireStaple(float angle) {
        openTimer = openTime;
        myShake = shakeAmount;
        staplerSound.Play();
        Instantiate(projectile, transform);
    }
}
