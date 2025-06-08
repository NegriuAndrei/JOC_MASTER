using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class BossHealth : MonoBehaviour
{

    [Header("Health UI")]
    public GameObject BossHPUI;
    public Slider bossHPSlider;
    public Sprite bossImage;
    public Sprite bossEnrangeImage;
    public Image bossIconUI;

    public Animator enemyanim;
    public int maxHealth = 100;
    public int currentHealth;
    public int EXPToGive;


    [Header("Loot Table")]
    public GameObject item1Drop;
    public float item1DropChance;
    public GameObject item2Drop;
    public float item2DropChance;

    public bool isInvulnerable = false;

    [Header("Die EFFECT")]
    public GameObject dieExplosionFX;
    public Vector2 dieExplosionSize = new Vector2(2, 3);


    public GameObject deadFX;
    public GameObject ExitLevel ;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();
        //enemyanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    public void takeDamage(int damage)
    {

        if (isInvulnerable)
   
            return;
        

        currentHealth -= damage;
        BossHPUI.SetActive(true);
        if(currentHealth <= maxHealth/2)
        {
            GetComponent<Animator>().SetBool("Enrange",true);
        }

        //play hurt animation
       // enemyanim.SetTrigger("Hurt");
        AudioController.instance.EnemyDeathSFX(3);
        if (currentHealth <= 0)
        {

            Die();
            //ExitLevel.SetActive(true);
           
        }
    }

    void Die()
    {
        enemyanim.SetBool("IsDeath", true);
        StartCoroutine(BossDieBehavior());
        Destroy(gameObject,5f);
        //BoxCollider2D.Destroy(gameObject,0.1f);
        BossHPUI.SetActive(false);
    }

    public void UpdateHealth()
    {
        bossHPSlider.maxValue = maxHealth;
        bossHPSlider.value = currentHealth;

        if(currentHealth <= maxHealth / 2)
        {
            bossIconUI.sprite = bossEnrangeImage;
        }
        else
        {
            bossIconUI.sprite = bossImage;
        }

    }

    IEnumerator BossDieBehavior()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            Instantiate(dieExplosionFX, transform.position + new Vector3(Random.Range(-dieExplosionSize.x, dieExplosionSize.x), Random.Range(0,dieExplosionSize.y),0),Quaternion.identity);

            AudioController.instance.PlayerSFX(0);
            yield return new WaitForSeconds(0.5f);
        }
        for(int i = 0; i < 5; i++)
        {
            Instantiate(dieExplosionFX, transform.position + new Vector3(Random.Range(-dieExplosionSize.x, dieExplosionSize.x), Random.Range(0, dieExplosionSize.y), 0), Quaternion.identity);

            AudioController.instance.PlayerSFX(0);
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }
}
