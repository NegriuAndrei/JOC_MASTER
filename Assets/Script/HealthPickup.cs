using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.instance.RestoreHealth(healthToGive);
            //AudioController.instance.UISFX(3);
            Destroy(gameObject);

        }
    }
}
