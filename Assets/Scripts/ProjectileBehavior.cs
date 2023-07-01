using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject self;
    private SpriteRenderer SpriteRenderer;

    public Projectile ProjectileClass;

    private int lifetime = 60;

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
        lifetime--;
        if(lifetime < 0 )
        {
            Destroy( self );
        }
        transform.localPosition = new Vector2(transform.position.x + ProjectileClass.Speed * transform.localScale.x, transform.position.y);
    }
}
