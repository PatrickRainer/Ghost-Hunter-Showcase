﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    #region SingletonPattern
    private static MusicController _instance;
    public static MusicController Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    #region Members

    public Slider volumeSlider;
    private AudioSource myAudiosource;

    #endregion

    #region Initializing
    private void Start()
    {
        myAudiosource = GetComponent<AudioSource>();
        LoadVolumeFromPrefs();
        volumeSlider.value = myAudiosource.volume;   
    }
    #endregion

    #region Methods

    private void LoadVolumeFromPrefs()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            myAudiosource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    private void StopMusic()
    {
        myAudiosource.Stop();
    }

    private void OnDestroy()
    {
        SaveVolumeToPrefs();
    }

    private void SaveVolumeToPrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", myAudiosource.volume);
    }
    #endregion
}