using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject winScreen;
    public float winDelay, timeToExit;
    public int nextSceneLoad;

    public Animator anim;

    private void Start()
    {
        // MODIFICARE: trece la nivelul următor
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        anim = GetComponent<Animator>();
    }

    public void WinGame()
    {
        StartCoroutine(WinGameco());
    }

    public IEnumerator WinGameco()
    {
        yield return new WaitForSeconds(winDelay);

        if (winScreen != null)
            winScreen.SetActive(true);

        if (AudioController.instance != null)
            AudioController.instance.PlayerSFX(13);

        yield return new WaitForSeconds(timeToExit);

        // Încarcă următorul nivel
        SceneManager.LoadScene(nextSceneLoad);

        if (AudioController.instance != null)
            AudioController.instance.levelMusic.Stop();

        // Salvează progresul (doar dacă nivelul curent e mai mare)
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt", 1))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WinGame();
        }
    }
}
