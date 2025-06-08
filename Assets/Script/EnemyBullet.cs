using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public Vector2 moveDiectionEnemy;
    private Rigidbody2D bulletRB;
    public int bulletDamage;
    private Animator anim;
    private SpriteRenderer bulletSprite;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = moveDiectionEnemy * bulletSpeed;

        if (bulletRB.velocity.x < 0)
        {
            bulletSprite.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D bulletHit)
    {
        //bullet Damage
        if (bulletHit.tag == "Player")
        {
            bulletHit.GetComponent<Player>().TakeDamage(bulletDamage);
            anim.SetBool("Hit", true);
            bulletSpeed = 0;
            Destroy(gameObject, 0.7f);
            AudioController.instance.PlayerSFX(1);
        }
        else
        {
            anim.SetBool("Hit", true);
            bulletSpeed = 0;
            Destroy(gameObject, 0.7f);
            AudioController.instance.PlayerSFX(1);
        }


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
