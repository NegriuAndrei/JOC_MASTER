using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
public class Player : MonoBehaviour
{
    public static Player instance;

    public bool rageMode;

    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;
    public float healthRegenSpeed;

    [Header("Health UI")]
    public Slider healthSlider;
    public TMP_Text healthText;

    [Header("Magic")]
    public float maxMagic = 100;
    public float currentMagic;
    public float magicRegenSpeed;
    public float bulletMagicCost;

    [Header("Magic UI")]
    public Slider magicSlider;
    public TMP_Text magicText;


    [Header("Rage")]
    public float maxRage;
    public float currentRage;
    public float rageRegenSpeed;
    public float derangeRage;

    [Header("Rage UI")]
    public Slider rageSlider;
    public TMP_Text rageText;

    [Header("EXP UI")]
    public Slider currentXpSlider;
    public TMP_Text levelText;

    public bool isDead;

    [Header("Movement")]
    public float runSpeed;
    public Animator playerAnimator;
    private Rigidbody2D playerRigidBody;

    //public FixedJoystick variableJoystick;

    [Header("Jump")]
    public float jumpForce;
    public Transform groundcheck;
    public bool isGrounded;
    public LayerMask groundFloor;
    private bool doDoubleJump;
    public GameObject Jumpeffect;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask BossLayers;
    public int attackDamage = 21;
    public float attackRate = 2f ;
    public float nextAttackTime = 0f;

    [Header("Range Attack")]
    public PlayerBullet bullet;
    public Transform shootingPoint;

    [Header("Level up System")]
    public int playerLevel = 1;
    public int[] expToNextLevel;
    public int maxLevel = 50;
    public int currentEXP;
    public int baseEXP = 500;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLevel = SaveManager.instance.activeSave.playerLevel;
        expToNextLevel = SaveManager.instance.activeSave.expToNextLevel;
        maxLevel = SaveManager.instance.activeSave.maxLevel;
        currentEXP = SaveManager.instance.activeSave.currentEXP;
        baseEXP = SaveManager.instance.activeSave.baseEXP;
        attackDamage = SaveManager.instance.activeSave.attackDamage;

        healthRegenSpeed = SaveManager.instance.activeSave.healthRegenspeed;
        magicRegenSpeed = SaveManager.instance.activeSave.magicRegenspeed;

        maxHealth = SaveManager.instance.activeSave.maxHealth;
        maxMagic = SaveManager.instance.activeSave.maxMagic;

        UpdateMagic();
        UpdateHealth();
        currentHealth = maxHealth;
        currentMagic = maxMagic;
        currentRage = maxRage;
        //Get Component
        playerRigidBody = GetComponent<Rigidbody2D>();

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i=2; i< expToNextLevel.Length;i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.2f);
        }

        // Button jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
        // jumpButton.onClick.AddListener(Jump);
        

    }

    // Update is called once per frame
    void Update()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        UpdateLevel();
        healthRegen();
        RegenMagic();
        RegenRage();
        UpdateHealth();
        UpdateMagic();
        UpdateRage();

        if (rageMode)
        {
            Derage();
        }

         if (!isDead)
         {

            //player movment
          

            //playerRigidBody.AddForce(direction.normalized * runSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            playerRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") *runSpeed, playerRigidBody.velocity.y);

            /*//Flipping the player
            if (playerRigidBody.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (playerRigidBody.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }*/



            if (playerRigidBody.velocity.x > 0)
            {
                // Asigurã-te cã X rãmâne pozitiv
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (playerRigidBody.velocity.x < 0)
            {
                // Flip: schimbã semnul lui X
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }


            //groung checkpoint
            isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.1f, groundFloor);


            //jumping


            if(Input.GetButtonDown("Jump") && (isGrounded|| doDoubleJump))
            {
                if(isGrounded)
                {
                    doDoubleJump = true;
                }else
                {
                    doDoubleJump = false;
                    playerAnimator.SetTrigger("Double Jump");
                }
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
            }







            playerAnimator.SetBool("IsGround", isGrounded);
            playerAnimator.SetFloat("Speed", Mathf.Abs(playerRigidBody.velocity.x));


            //shooting


            if(Time.time >=nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MeeleAttack();
                    nextAttackTime = Time.time + 1f/attackRate;
                }
            }
         }
         else
         {
             playerRigidBody.velocity = Vector2.zero;
         }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUP(300);
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
           
                MeeleAttack();
                nextAttackTime = Time.time + 1f / attackRate;
       
        }
    }

    public void ShootButtonPress()
    {
        if (Time.time >= nextAttackTime)
        {
           if(currentMagic >= bulletMagicCost && rageMode)

            {
                Player.instance.playerAnimator.SetTrigger("Spell");


                Instantiate(Player.instance.bullet, Player.instance.shootingPoint.position, Player.instance.shootingPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
                AudioController.instance.PlayerSFX(0);
                Player.instance.currentMagic -= Player.instance.bulletMagicCost;
                Player.instance.nextAttackTime = Time.time + 1f / Player.instance.attackRate;
                //ShootBoss.instance.shoot();
            }

        }
    }

    

    public void LevelUP(int XP)
    {
        currentEXP += XP;
        SaveManager.instance.activeSave.playerLevel = playerLevel;
        SaveManager.instance.activeSave.expToNextLevel = expToNextLevel;
        SaveManager.instance.activeSave.currentEXP = currentEXP;
        UpdateLevel();

        if(playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;
                SaveManager.instance.activeSave.playerLevel = playerLevel;
                SaveManager.instance.activeSave.expToNextLevel = expToNextLevel;
                SaveManager.instance.activeSave.currentEXP = currentEXP;
                UpdateLevel();


                maxHealth += 10;
                SaveManager.instance.activeSave.maxHealth = Player.instance.maxHealth;
                maxMagic += 5;
                SaveManager.instance.activeSave.maxMagic = Player.instance.maxMagic;
                attackDamage += 5;
                SaveManager.instance.activeSave.attackDamage = Player.instance.attackDamage;
                currentHealth = maxHealth;
                currentMagic = maxMagic;

            }
        }
        else
        {
            currentEXP = 0;
            SaveManager.instance.activeSave.currentEXP = currentEXP;
        }
       
    }

    public void MeeleAttack()
    {
        playerAnimator.SetTrigger("Attack");
        //AudioController.instance.PlayerSFX(8);
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitBosses =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
        Debug.Log("---------------------------------" + hitEnemies.Length);

            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
        foreach (Collider2D boss in hitBosses)
        {
            boss.GetComponent<BossHealth>().takeDamage(attackDamage);
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;

        playerAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealth();
       
    }

    public void UpdateRage()
    {
        rageSlider.maxValue = maxRage;
        rageSlider.value = currentRage;
        rageText.text = Mathf.RoundToInt(currentRage) + "/" + maxRage;
    }
    public void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = Mathf.RoundToInt (currentHealth) + "/" + maxHealth;
    }

    public void UpdateMagic()
    {
        magicSlider.maxValue = maxMagic;
        magicSlider.value = currentMagic;
        magicText.text = Mathf.RoundToInt (currentMagic) + "/" + maxMagic;
    }

    public void Derage()
    {
        currentRage -= derangeRage * Time.deltaTime;

        if (currentRage <= 0)
        {
            rageMode = false;
            playerAnimator.SetBool("Rage", false);
            
        }
        UpdateRage();
    }

    public void RegenRage()
    {

        if (!rageMode)
        {
            currentRage += rageRegenSpeed * Time.deltaTime;
            if (currentRage > maxRage)
            {
                currentRage = maxRage;
            }
        }
       
    }

    public void UpdateLevel()
    {
        if (playerLevel < maxLevel)
        {
            currentXpSlider.maxValue = expToNextLevel[playerLevel];
            currentXpSlider.value = currentEXP;
            levelText.text = "LV" + playerLevel;
        }
        if(playerLevel == maxLevel)
        {
            currentXpSlider.maxValue = 0;
            levelText.text = "MAX LV";
        }
        
    }

    public void RegenMagic()
    {
        currentMagic += magicRegenSpeed * Time.deltaTime;
        if(currentMagic > maxMagic)
        {
            currentMagic = maxMagic;
        }
    }
    public void healthRegen()
    {
        currentHealth += healthRegenSpeed * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        playerAnimator.SetBool("IsDeath", true);
        AudioController.instance.PlayerSFX(5);
        GetComponent<Collider2D>().enabled = false;
        GameManager.instance.GameOver();
        isDead = true;
       
    }

    public void RestoreHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }

    public void RangeMode()
    {
        rageMode = true;
        playerAnimator.SetBool("Rage", true);
       
    }
    public void UnrangeMode()
    {
        rageMode = false;
        playerAnimator.SetBool("Rage", false);

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
