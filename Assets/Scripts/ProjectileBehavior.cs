using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject self;
    private SpriteRenderer SpriteRenderer;

    public Projectile ProjectileClass;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        SpriteRenderer = self.GetComponent<SpriteRenderer>();
        ProjectileClass.setSprite(SpriteRenderer.sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + ProjectileClass.Speed, transform.position.y);
    }
}
