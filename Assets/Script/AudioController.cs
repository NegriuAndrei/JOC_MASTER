using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }else
        {
            Destroy(gameObject);

        }
    }

    public AudioSource mainMenuMusic;
    public AudioSource levelMusic;
    public AudioSource BossMusic;
    public AudioSource[] playersfx;
    public AudioSource[] enemyDeathsfx;
    public AudioSource[] Uisfx;
    public AudioSource[] efectsfx;



    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        BossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        levelMusic.Play();
        mainMenuMusic.Stop();
        BossMusic.Stop();

    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        mainMenuMusic.Stop();
        BossMusic.Play();
    }

   public void PlayerSFX(int sfxPlayer)
    {
        playersfx[sfxPlayer].Stop();
        playersfx[sfxPlayer].Play();
    }
    public void EnemyDeathSFX(int sfxEnemy)
    {
        enemyDeathsfx[sfxEnemy].Stop();
        enemyDeathsfx[sfxEnemy].Play();
    }
    public void UISFX(int sfxUI)
    {
        Uisfx[sfxUI].Stop();
        Uisfx[sfxUI].Play();
    }
    public void EfectsSFX(int sfxEfects)
    {
        efectsfx[sfxEfects].Stop();
        efectsfx[sfxEfects].Play();
    }
}
