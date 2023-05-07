using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UI_Manager _UIManager;

    [SerializeField]
    GameObject _pauseMenu;

    [SerializeField]
    GameObject _player;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioSource _bmgAudio;

    [SerializeField]
    AudioClip _menuClickClip;

    [SerializeField]
    AudioClip _playerDeathSFX;

    [SerializeField]
    GameObject _deathVFX;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!_pauseMenu.activeSelf)
            {
                _UIManager.OpenPauseMenu();
                _sfxAudio.PlayOneShot(_menuClickClip, 1f);
            }
            else
            {
                _UIManager.Resume();
                _sfxAudio.PlayOneShot(_menuClickClip, 1f);
            }
        }
    }

    public IEnumerator KillPlayer()
    {
        _player.SetActive(false);
        Instantiate(_deathVFX, _player.transform.position, Quaternion.identity);
        _bmgAudio.volume = 0;
        _sfxAudio.PlayOneShot(_playerDeathSFX, 1f);
        yield return new WaitForSeconds(2.5f);
        _bmgAudio.volume = 1;
        _UIManager.OpenGameoverMenu();
    }
}
