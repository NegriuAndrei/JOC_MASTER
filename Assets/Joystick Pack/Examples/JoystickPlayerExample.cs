using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}

/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody2D rb; // Modificare aici: utilizăm Rigidbody2D în loc de Rigidbody

    public void FixedUpdate()
    {
        Vector2 direction = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical); // Modificare aici: utilizăm Vector2 în loc de Vector3
        rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode2D.Impulse); // Modificare aici: utilizăm ForceMode2D.Impulse în loc de ForceMode.VelocityChange
    }
}

 * */