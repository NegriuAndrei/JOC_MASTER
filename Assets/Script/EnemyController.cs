using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool shouldPatrol;
    public bool shouldShoot;
    private float nextTimeAttack = 0f;
    private float attackRate = 1f;
    public float attackDistance; //minimum Distance for attack
    public float moveSpeed;     //enemy move speed
    public float timer; //timer for cooldown between attacks
    public Transform leftLimit;  //patrol point
    public Transform rightLimit;  // patrol point 
    public GameObject hotZone;    //alarm zone for detecting player
    public GameObject triggerArea;

    public bool isDead;

    public Transform groundcheck;
    private bool isGrounded;
    public LayerMask groundFloor;

    public Transform target;
    private Animator anim;
    private float distance; //store the distance between enemy and player
    private bool attackMode;
    public bool inRange;//check if player is in range
    private bool cooling;//checked if enemy is cooling after attack
    private float intTimer;//store initial timer

    [Header("Range Attack")]
    public EnemyBullet bullet;
    public Transform shootingPoint;
    private void Awake()
    {
        intTimer = timer; //store the initial value of timer
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.1f, groundFloor);

        if (!isDead)
        {
            if (!attackMode && isGrounded)
            {
                Move();
            }
            else if (!isGrounded)
            {
                Patrol();
                Move();
            }

            if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
            {
                Patrol();
            }

            if (inRange)
            {
                EnemyLogic();
            }

            
        }

        
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        
            if (distance > attackDistance)
            {
                StopAttack();
            }
        
            else if (attackDistance >= distance && cooling == false)
            {
                Attack();
            }

            if (cooling)
            {
                cooldown();
                anim.SetBool("Attack", false);
            }
       
        
    }

    void Move()
    {
        if(target != null)
        {
            if (shouldPatrol)
            {
                anim.SetBool("Walk", true);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack") && !inRange)
                {
                    Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
                else
                {
                    Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, (moveSpeed + 2f) * Time.deltaTime);
                }

            }
        }
        
       
    }

    void Attack()
    {
        

        if (!shouldShoot)
        {
            isGrounded = true;
            timer = intTimer; //reset timer when player enter attack range
            attackMode = true;//to check if enemy can still attack or not

            anim.SetBool("Walk", false);
            anim.SetBool("Attack", true);

        }
        else
        {
            if (Time.time >= nextTimeAttack)
            {
                anim.SetTrigger("Attack");
                Shooting();
                nextTimeAttack = Time.time + 1f / attackRate;
                anim.ResetTrigger("Attack");
            }
              
        }
    }
    

    public void Shooting()
    {
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation).moveDiectionEnemy = new Vector2(transform.localScale.x, 0);
        AudioController.instance.PlayerSFX(0);
    }

    void cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void Patrol()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {

        if(target != null)
        {
            if(transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        /*Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;*/
    }

    /*public void shoot()
    {
        if (shouldShoot)
        {
           
                Instantiate(bullet, shootingPoint.position, shootingPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
                AudioController.instance.PlayerSFX(0);

        }
    }*/
}
