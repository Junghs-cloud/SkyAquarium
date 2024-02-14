using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class setting : MonoBehaviour
{
    public static setting instance;
    public Button leftButton;
    public Button rightButton;
    public AudioClip[] bgmlist;
    public AudioSource audioSource;

    public int bgmNum;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public TMP_Text sfxVolumeText;
    public TMP_Text bgmVolumeText;

    public TMP_Text bgmNameText;
    public AudioSource SFXPlayer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.mute = false;
        SFXPlayer.loop = false;
        leftButton.onClick.AddListener(leftClick);
        rightButton.onClick.AddListener(rightClick);
        playBGM();
    }

    void leftClick()
    {
        bgmNum--;
        if (bgmNum == -1)
        {
            bgmNum = bgmNum + 8;
        }
        while (playerData.instance.musicUnLock[bgmNum] == false)
        {
            bgmNum--;
            if (bgmNum == -1)
            {
                bgmNum = bgmNum + 8;
            }
        }
        playerData.instance.currentBGM = bgmNum;
        dataManager.instance.saveToJson();
        playBGM();
    }

    void rightClick()
    {
        bgmNum++;
        if (bgmNum == 8)
        {
            bgmNum = bgmNum - 8;
        }
        while (playerData.instance.musicUnLock[bgmNum] == false)
        {
            bgmNum++;
            if (bgmNum == 8)
            {
                bgmNum = bgmNum - 8;
            }
        }
        playerData.instance.currentBGM = bgmNum;
        dataManager.instance.saveToJson();
        playBGM();
    }

    void playBGM()
    {
        audioSource.clip = bgmlist[bgmNum];
        audioSource.Play();
        string bgmName = bgmlist[bgmNum].name;
        bgmNameText.text = "BGM " + (bgmNum + 1).ToString()+": "+bgmName;
    }

    public void updateSFXVolume(Slider slider)
    {
        int volumePercent = (int)(slider.value * 100);
        playerData.instance.SFXVolume = slider.value;
        sfxVolumeText.text = volumePercent + "%";
        dataManager.instance.saveToJson();
    }

    public void updateBGMVolume(Slider slider)
    {
        audioSource.volume = slider.value;
        int volumePercent = (int) (slider.value * 100);
        playerData.instance.BGMVolume = slider.value;
        bgmVolumeText.text = volumePercent+"%";
        dataManager.instance.saveToJson();
    }

    public void setSFXandBGMVolume(float sfxVolume, float bgmVolume)
    {
        sfxVolumeSlider.value = sfxVolume;
        bgmVolumeSlider.value = bgmVolume;
        sfxVolumeText.text = (int) (sfxVolume * 100) + "%";
        bgmVolumeText.text =(int) (bgmVolume * 100) + "%";
    }

    public void playFishSplashSFX()
    {
        SFXPlayer.Play();
    }
}
