using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class levelSystem : MonoBehaviour
{
    [Serializable]
    public struct aboutLevel
    {
        public string name;
        public int NeededEXPForRankUp;
        public Sprite[] unlockObjectImages;
        public shopCell[] unlockShopCell;
    };

    [SerializeField]
    public List<aboutLevel> levelSystemList;

    public GameObject rankUpPanel;
    public GameObject contentPanel;
    public TMP_Text rankText;

    public static levelSystem instance;
    public int currentLevelNeededEXP;

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    void Start()
    {
        aboutLevel currentLevel = levelSystemList.Find(x => x.name == "rank " + playerData.instance.rank);
        currentLevelNeededEXP = currentLevel.NeededEXPForRankUp;
    }

    public void unLockObjectsWithRank(int rank)
    {
        aboutLevel currentLevel = levelSystemList.Find(x => x.name == "rank " + rank.ToString());
        currentLevelNeededEXP = currentLevel.NeededEXPForRankUp;
        unlockShopCells(currentLevel);
        showUnlockObjectImages(currentLevel);
        rankText.text = "RANK " + rank.ToString();
        rankUpPanel.SetActive(true);
    }

    void unlockShopCells(aboutLevel currentLevel)
    {
        foreach (shopCell currentUnlockCell in currentLevel.unlockShopCell)
        {
            currentUnlockCell.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    void showUnlockObjectImages(aboutLevel currentLevel)
    {
        for (int i = 0; i < currentLevel.unlockObjectImages.Length; i++)
        {
            Transform child = contentPanel.transform.GetChild(i);
            Transform settingImageChild = child.GetChild(0);
            settingImageChild.GetComponent<Image>().sprite = currentLevel.unlockObjectImages[i];
        }
        for (int i = currentLevel.unlockObjectImages.Length; i < contentPanel.transform.childCount; i++)
        {
            contentPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
