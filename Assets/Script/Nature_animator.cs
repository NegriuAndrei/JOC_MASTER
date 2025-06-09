using UnityEngine;

public class NatureMonsterAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Enemy enemyScript;

    public float movementThreshold = 0.1f; // cât de mult trebuie să se miște ca să considere că merge

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Animatorul e pe Graphics
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();
    }

    void Update()
    {
        // Setează isWalking dacă se mișcă
        if (Mathf.Abs(rb.velocity.x) > movementThreshold)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Dacă vrei să atace după un trigger (de exemplu când ajunge în apropiere de jucător)
        // poți seta isAttacking în alt script și îl citim aici, dar momentan o simulăm:
        // animator.SetBool("isAttacking", true/false); => Lasă-l pentru un pas ulterior
    }

    public void TriggerAttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        Invoke("ResetAttack", 0.8f); // Revenim la idle după 0.8 secunde (poți ajusta)
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
