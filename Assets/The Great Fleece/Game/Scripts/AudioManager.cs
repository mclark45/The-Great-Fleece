using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Audio Manager is Null");
            }
            return _instance;
        }
    }

    [SerializeField]
    private AudioSource _voiceOver;
    [SerializeField]
    private AudioSource _coinSound;
    [SerializeField]
    private AudioSource _music;


    private void Awake()
    {
        _instance = this;
    }

    public void PlayVoiceOver(AudioClip clipToPlay)
    {
        _voiceOver.clip = clipToPlay;
        _voiceOver.Play();
    }

    public void CoinSound(AudioClip coin)
    {
        _coinSound.clip = coin;
        _coinSound.Play();
    }

    public void PlayMusic()
    {
        _music.Play();
    }
}
