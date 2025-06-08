using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{


    public int coinToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.GetCoins(coinToGive);
            AudioController.instance.UISFX(1);
            Destroy(gameObject);

        }
    }
}
