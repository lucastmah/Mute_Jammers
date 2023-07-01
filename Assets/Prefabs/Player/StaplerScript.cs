using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerScript : MonoBehaviour
{
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite close;
    [SerializeField] private SpriteRenderer myRenderer;

    [SerializeField] private int openTime = 20; // time for the sprite to stay open in frames
    private int openTimer;

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
    }

    public void FireStaple() {
        openTimer = openTime;
    }
}
