using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class marineAnimal
{
    public int instanceID;
    public string marineAnimalName;
    public string instantiateObjectName;
    public int level;
    public int levelProgress;
    public long lastCollect;

    public marineAnimal(int instanceID, string marineAnimalName, string intstantiateObjectName, int level, int levelProgress, long lastCollect)
    {
        this.instanceID = instanceID;
        this.marineAnimalName = marineAnimalName;
        this.instantiateObjectName= intstantiateObjectName;
        this.level = level;
        this.levelProgress = levelProgress;
        this.lastCollect = lastCollect;
    }
}

[Serializable]
public class Item
{
    public string itemName;
    public string itemSpriteName;
    public int itemCount;

    public Item(string itemName, string itemSpriteName, int itemCount)
    {
        this.itemName = itemName;
        this.itemSpriteName = itemSpriteName;
        this.itemCount = itemCount;
    }
}

[Serializable]
public class groundItem
{
    public string itemName;
    public string itemSpriteName;
    public float x;
    public float y;

    public groundItem(string itemName, string itemSpriteName, float x, float y)
    {
        this.itemName = itemName;
        this.itemSpriteName = itemSpriteName;
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> itemList;
    public List<T> ToList()
    {
        return itemList;
    }
    public Serialization(List<T> itemList)
    {
        this.itemList = itemList;

    }
}

[Serializable]
public class playerData : MonoBehaviour
{
    public static playerData instance;
    [Header("-Player Inforamtion-")]
    public string nickname;
    public int rank;
    public int EXP;

    public int money;
    public int diamond;
    public int food;

    public int currentSeaAnimal;
    public int maxSeaAnimal;

    public List<marineAnimal> marineAnimals;
    public List<Item> inventory;
    public List<groundItem> groundItems;
    public List<inventoryCell> inventoryCells;

    [Space(10f)]
    [Header("-About Shop ETC Section-")]
    public bool hasMoonBackground;
    public int fishAmountLevel;

    [Space (10f)]
    [Header("-About Sound-")]
    public bool[] musicUnLock = new bool[8];
    public int currentBGM;
    public float SFXVolume;
    public float BGMVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        marineAnimals = new List<marineAnimal>();
        inventory = new List<Item>();
        groundItems = new List<groundItem>();
        inventoryCells = new List<inventoryCell>();
        hasMoonBackground = false;
        currentBGM = 0;
        setMusicUnLock();
    }

    void setMusicUnLock()
    {
        for (int i = 0; i < 4; i++)
        {
            musicUnLock[i] = true;
        }
        for (int i = 4; i < 8; i++)
        {
            musicUnLock[i] = false;
        }
    }

    public void setMoney(int newMoney)
    {
        money = newMoney;
        playerUIManager.instance.updatePlayerAssetTMP();
    }

    public void setDiamond(int newDiamond)
    {
        diamond = newDiamond;
        playerUIManager.instance.updatePlayerAssetTMP();
    }

    public void setFood(int newFood)
    {
        food = newFood;
        playerUIManager.instance.updatePlayerAssetTMP();
    }

    public void setCurrentSeaAnimal(int newSeaAnimalCount)
    {
        currentSeaAnimal = newSeaAnimalCount;
        playerUIManager.instance.updateCurrentSeaAnimalTMP();
    }

    public void setMaxSeaAnimal(int newMaxSeaAnimal)
    {
        maxSeaAnimal = newMaxSeaAnimal;
        playerUIManager.instance.updateMaxSeaAnimalTMP();
    }

    public void setEXP(int newEXP)
    {
        EXP = newEXP;
        if (EXP >= levelSystem.instance.currentLevelNeededEXP)
        {
            rank++;
            EXP -= levelSystem.instance.currentLevelNeededEXP;
            levelSystem.instance.unLockObjectsWithRank(rank);
            playerUIManager.instance.changeRankText();
            setting.instance.playSFX(setting.sfx.rankUp);
        }
        playerUIManager.instance.updateEXPSlider();
    }

    public void setPlayerNickname(string playerNickname)
    {
        nickname = playerNickname;
        playerUIManager.instance.setPlayerNickname();
    }
}
