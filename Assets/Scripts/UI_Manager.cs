using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject _creditsMenu;

    [SerializeField]
    GameObject _mainMenu;

    [SerializeField]
    GameObject _pauseMenu;

    [SerializeField]
    GameObject _gameoverMenu;

    [SerializeField]
    GameObject _audioMenu;

    [SerializeField]
    SquareManager _squareManager;

    [SerializeField]
    TextMeshProUGUI _cratesText;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    [SerializeField]
    TextMeshProUGUI _bestScoreText;

    [SerializeField]
    GameObject _zoomBar;

    [SerializeField]
    GameObject _tutorialMenu;

    [SerializeField]
    TextMeshProUGUI _gameOverScoreText;

    [SerializeField]
    GameObject _gameOverBestScoreText;

    [SerializeField]
    AudioSource[] _backgroundAudio;

    [SerializeField]
    AudioSource _gameOverAudio;

    [SerializeField]
    AudioClip[] _gameOverClip;

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
        _creditsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        _creditsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

        if (_squareManager.ScoreTotal > _squareManager.BestScore)
        {
            PlayerPrefs.SetFloat("BestScore", _squareManager.ScoreTotal);
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _audioMenu.SetActive(false);

        for (int i = 0; i < _backgroundAudio.Length; i++)
            _backgroundAudio[i].Pause();
    }

    public void OpenGameoverMenu()
    {
        Time.timeScale = 0;
        _gameoverMenu.SetActive(true);
        _audioMenu.SetActive(false);
        _cratesText.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _bestScoreText.gameObject.SetActive(false);
        _zoomBar.SetActive(false);
        _tutorialMenu.SetActive(false);

        _gameOverScoreText.text = _squareManager.ScoreTotal.ToString();

        if (_squareManager.ScoreTotal > _squareManager.BestScore)
        {
            _gameOverBestScoreText.SetActive(true);

            PlayerPrefs.SetFloat("BestScore", _squareManager.ScoreTotal);
        }
        else
        {
            _gameOverBestScoreText.SetActive(false);
        }

        for (int i = 0; i < _backgroundAudio.Length; i++)
            _backgroundAudio[i].Stop();

        _gameOverAudio.clip = _gameOverClip[Random.Range(0, _gameOverClip.Length)];
        _gameOverAudio.Play();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _audioMenu.SetActive(true);

        for (int i = 0; i < _backgroundAudio.Length; i++)
            _backgroundAudio[i].UnPause();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

        if (_squareManager.ScoreTotal > _squareManager.BestScore)
        {
            PlayerPrefs.SetFloat("BestScore", _squareManager.ScoreTotal);
        }
    }

    public void CrateAmount(float amountOfCrates)
    {
        _cratesText.text = amountOfCrates.ToString();
    }

    public void WriteScore(float score)
    {
        _scoreText.text = score.ToString();
    }

    public void WriteBestScore(float score)
    {
        _bestScoreText.text = score.ToString();
    }
}
