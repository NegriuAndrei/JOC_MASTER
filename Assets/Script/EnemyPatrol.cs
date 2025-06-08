using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool movingRight = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError("🔴 LeftPoint sau RightPoint NU sunt setate!");
        }
    }

    void FixedUpdate()
    {
        if (leftPoint == null || rightPoint == null) return;

        Vector2 newPos;

        if (movingRight)
        {
            newPos = rb.position + Vector2.right * speed * Time.fixedDeltaTime;

            if (rb.position.x >= rightPoint.position.x)
            {
                Debug.Log("⬅️ Schimbă direcția spre stânga");
                movingRight = false;
                sprite.flipX = true;
            }
        }
        else
        {
            newPos = rb.position + Vector2.left * speed * Time.fixedDeltaTime;

            if (rb.position.x <= leftPoint.position.x)
            {
                Debug.Log("➡️ Schimbă direcția spre dreapta");
                movingRight = true;
                sprite.flipX = false;
            }
        }

        rb.MovePosition(newPos);
    }
}
