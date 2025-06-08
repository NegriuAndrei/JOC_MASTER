using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Health UI")]
    public Slider healthSlider;

    public Animator enemyanim;
    public int maxHealth = 100;
    int currentHealth;
    public int EXPToGive;


    [Header("Loot Table")]
    public GameObject item1Drop;
    public float item1DropChance;
    public GameObject item2Drop;
    public float item2DropChance;
    public GameObject item3Drop;
    public float item3DropChance;

    private EnemyController parentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();

        parentEnemy = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        
        //play hurt animation
        enemyanim.SetTrigger("Hurt");
        //AudioController.instance.EnemyDeathSFX(3);
        if(currentHealth <= 0)
        {
            parentEnemy.isDead = true;
            Die();
           // AudioController.instance.EnemyDeathSFX(3);
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        //die animation and destroy it
        enemyanim.SetBool("Dead", true);
        //disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
        Destroy(gameObject, 1.4f);
        Player.instance.LevelUP(EXPToGive);

        if(Random.Range(0f,100f) < item1DropChance)
        {
            Instantiate(item1Drop, transform.position, transform.rotation);
        }
        if (Random.Range(0f, 100f) < item2DropChance)
        {
            Instantiate(item2Drop, transform.position, transform.rotation);
        }
        if (Random.Range(0f, 100f) < item3DropChance)
        {
            Instantiate(item3Drop, transform.position, transform.rotation);
        }

    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        

        
    }

}
