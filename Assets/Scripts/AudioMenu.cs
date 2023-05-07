using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider soundSlider;

    public AudioSource bgmAudio;
    public AudioClip[] tracks;
    public int currentTrack = 0;

    public TextMeshProUGUI trackText;
    public TextMeshProUGUI trackTitleText;

    [SerializeField]
    private GameObject _pauseMenu;

    void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);

        currentTrack = Random.Range(0, tracks.Length);
        bgmAudio.clip = tracks[currentTrack];
        bgmAudio.Play();
        trackText.text = (currentTrack + 1) + "/" + tracks.Length;
        ChangeTrackTitle(currentTrack);
    }

    void Update()
    {
        if (_pauseMenu != null && !bgmAudio.isPlaying && !_pauseMenu.activeSelf)
        {
            ChangeTrackForward();
        }
        else if (_pauseMenu == null && !bgmAudio.isPlaying)
        {
            ChangeTrackForward();
        }
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void ChangeTrackForward()
    {
        if (currentTrack < tracks.Length - 1)
        {
            currentTrack++;
        }
        else
        {
            currentTrack = 0;
        }

        ChangeTrackTitle(currentTrack);

        bgmAudio.clip = tracks[currentTrack];
        bgmAudio.Play();

        trackText.text = (currentTrack + 1) + "/" + tracks.Length;
    }

    public void ChangeTrackBackwards()
    {
        if (currentTrack > 0)
        {
            currentTrack--;
        }
        else
        {
            currentTrack = tracks.Length - 1;
        }

        ChangeTrackTitle(currentTrack);

        bgmAudio.clip = tracks[currentTrack];
        bgmAudio.Play();

        trackText.text = (currentTrack + 1) + "/" + tracks.Length;
    }

    void ChangeTrackTitle(int currentTrack)
    {
        switch (currentTrack)
        {
            case 0:
                trackTitleText.text = "'Penguin Café Single'";
                break;
            case 1:
                trackTitleText.text = "'Zopf: From the Colonies'";
                break;
            case 2:
                trackTitleText.text = "'Zopf: Giles Farnaby's Dream'";
                break;
            case 3:
                trackTitleText.text = "'Pythagoras's Trousers'";
                break;
            case 4:
                trackTitleText.text = "'Salty Bean Fumble'";
                break;
            case 5:
                trackTitleText.text = "'Perpetuum Mobile'";
                break;
            case 6:
                trackTitleText.text = "'Bean Fields'";
                break;
            case 7:
                trackTitleText.text = "'In The Back Of A Taxi'";
                break;
            case 8:
                trackTitleText.text = "'Dirt'";
                break;
            case 9:
                trackTitleText.text = "'Paul's Dance'";
                break;
            case 10:
                trackTitleText.text = "'Scherzo And Trio'";
                break;
            case 11:
                trackTitleText.text = "'Music For A Found Harmonium'";
                break;
            default:
                trackTitleText.text = "";
                break;
        }
    }
}
