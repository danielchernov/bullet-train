using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject creditsMenu;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    AudioSource[] backgroundAudio;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        for (int i = 0; i < backgroundAudio.Length; i++)
            backgroundAudio[i].Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        for (int i = 0; i < backgroundAudio.Length; i++)
            backgroundAudio[i].UnPause();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
