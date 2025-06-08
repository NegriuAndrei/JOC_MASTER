using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPLayer : MonoBehaviour
{
    public int damagePlayer;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            other.GetComponent<Player>().TakeDamage(damagePlayer);
        }
    }
}
