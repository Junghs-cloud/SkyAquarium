using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class setting : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public AudioClip[] bgmlist;
    public AudioSource audioSource;

    int bgmNum;
    public TMP_Text sfxVolumeText;
    public TMP_Text bgmVolumeText;

    public TMP_Text bgmNameText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.mute = false;
        leftButton.onClick.AddListener(leftClick);
        rightButton.onClick.AddListener(rightClick);
        bgmNum = 0;
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
        sfxVolumeText.text = volumePercent + "%";
    }

    public void updateBGMVolume(Slider slider)
    {
        audioSource.volume = slider.value;
        int volumePercent = (int) (slider.value * 100);
        bgmVolumeText.text = volumePercent+"%";
    }
}
