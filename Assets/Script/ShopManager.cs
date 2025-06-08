using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public static ShopManager instance;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveManager.instance.activeSave.currentCoins = GameManager.instance.currentCoins;
    }

    public void HealthUpgrade50()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.maxHealth += 50;
            Player.instance.currentHealth += 50;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.maxHealth = Player.instance.maxHealth;

            GameManager.instance.UpdateCoin();
            AudioController.instance.UISFX(6);
            Player.instance.UpdateHealth();
        }
        


    }

    public void MagicUpgrade5()
    {
        if (GameManager.instance.currentCoins >= 200)
        {
            Player.instance.maxMagic += 5;
            Player.instance.currentMagic += 5;
            GameManager.instance.currentCoins -= 200;
            SaveManager.instance.activeSave.maxMagic = Player.instance.maxMagic;
            GameManager.instance.UpdateCoin();
            AudioController.instance.UISFX(6);
            Player.instance.UpdateMagic();
        }
    }
    public void HealthRegenUpgrade()
    {
        if (GameManager.instance.currentCoins >= 500)
        {
            Player.instance.healthRegenSpeed += 1;
            GameManager.instance.currentCoins -= 500;
            SaveManager.instance.activeSave.healthRegenspeed = Player.instance.healthRegenSpeed;
            GameManager.instance.UpdateCoin();
            AudioController.instance.UISFX(6);
            Player.instance.healthRegen();
        }
    }
    public void MagicRegenUpgrade()
    {
        if (GameManager.instance.currentCoins >= 500)
        {
            Player.instance.magicRegenSpeed += 1;
            GameManager.instance.currentCoins -= 500;
            SaveManager.instance.activeSave.magicRegenspeed = Player.instance.magicRegenSpeed;
            GameManager.instance.UpdateCoin();
            AudioController.instance.UISFX(6);
            Player.instance.RegenMagic();
        }
    }

    public void AttackDamageUpgrade()
    {
        if (GameManager.instance.currentCoins >= 300)
        {
            Player.instance.attackDamage += 10;
            GameManager.instance.currentCoins -= 300;
            SaveManager.instance.activeSave.attackDamage = Player.instance.attackDamage;
            GameManager.instance.UpdateCoin();
            AudioController.instance.UISFX(6);
            
        }
    }
}
