using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marineAnimal
{
    public int instanceID;
    string marineAnimalName;
    public int level;
    public int levelProgress;
    public long lastCollect;

    public marineAnimal(int instanceID, string marineAnimalName, int level, int levelProgress, long lastCollect)
    {
        this.instanceID = instanceID;
        this.marineAnimalName = marineAnimalName;
        this.level = level;
        this.levelProgress = levelProgress;
        this.lastCollect = lastCollect;
    }
}

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

public class groundItem
{
    public string itemName;
    string itemSpriteName;
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

public class playerData : MonoBehaviour
{
    public static playerData instance;
    string nickname;
    public int rank;
    int exp;

    public int money;
    public int diamond;
    public int food;

    public int currentSeaAnimal;
    public int maxSeaAnimal;

    public List<marineAnimal> marineAnimals;
    public List<Item> inventory;
    public List<groundItem> groundItems;
    public List<inventoryCell> inventoryCells;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        marineAnimals = new List<marineAnimal>();
        inventory = new List<Item>();
        groundItems = new List<groundItem>();
        inventoryCells = new List<inventoryCell>();

        //testDatas
        groundItems.Add(new groundItem("산호 A-1", "coralA_1", -10.22f, -6.96f));
        groundItems.Add(new groundItem("산호 A-1", "coralA_1", -0.16f, -6.84f));
        groundItems.Add(new groundItem("산호 A-2", "coralA_2", -16.3f, -11.04f));
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
}
