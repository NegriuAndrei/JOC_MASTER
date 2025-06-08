using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{

    public ShootBoss instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player.instance.playerAnimator.SetTrigger("Spell");

        Instantiate(Player.instance.bullet, Player.instance.shootingPoint.position, Player.instance.shootingPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
        AudioController.instance.PlayerSFX(0);
        Player.instance.currentMagic -= Player.instance.bulletMagicCost;
        Player.instance.nextAttackTime = Time.time + 1f / Player.instance.attackRate;
    }

     void shoot()
    {
        Player.instance.playerAnimator.SetTrigger("Spell");
        
        Instantiate(Player.instance.bullet,Player.instance.shootingPoint.position,Player.instance.shootingPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
        AudioController.instance.PlayerSFX(0);
        Player.instance.currentMagic -=Player.instance.bulletMagicCost;
        Player.instance.nextAttackTime = Time.time + 1f / Player.instance.attackRate;
    }
    private void Awake()
    {
        instance = this;
    }
}
