using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class dataManager : MonoBehaviour
{
    public static dataManager instance;
    public GameObject inventoryContent;

    public GameObject nicknameSettingPanel;
    public TMP_InputField nicknameInputField;
    public GameObject blackBackground;
    public GameObject tutorialCanvas;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        loadFromJson();
    }

    public void saveToJson()
    {
        savePlayerInformation();
    }

    void savePlayerInformation()
    {
        string saveData = JsonUtility.ToJson(playerData.instance, true);
        string path = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(path, saveData);
    }

    public void loadFromJson()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "playerData.json");
            string saveData = File.ReadAllText(path);
            setPlayerData(saveData);
        }
        catch
        {
            blackBackground.SetActive(true);
            nicknameSettingPanel.SetActive(true);
            setNewPlayerData();
        }
    }

    void setPlayerData(string saveData)
    {
        JsonUtility.FromJsonOverwrite(saveData, playerData.instance);
        setSoundSetting();
        setMarineAnimals();
        setGroundItems();
        setInventory();
        if (playerData.instance.isTutorialFinished == false)
        {
            tutorialCanvas.SetActive(true);
            tutorial.instance.shopButton.interactable = false;
            tutorial.instance.currentTutorialLine = playerData.instance.currentTutorialLine - 1;
        }
    }

    void setMarineAnimals()
    {
        foreach (marineAnimal currentSeaAnimal in playerData.instance.marineAnimals)
        {
            GameObject seaAnimalObject = Resources.Load<GameObject>("Prefabs/seaAnimal/" + currentSeaAnimal.instantiateObjectName);
            GameObject generatedSeaAnimal = Instantiate(seaAnimalObject);
            currentSeaAnimal.instanceID = generatedSeaAnimal.GetInstanceID();
            seaAnimal seaAnimalScript = generatedSeaAnimal.GetComponent<seaAnimal>();
            seaAnimalScript.level = currentSeaAnimal.level;
            seaAnimalScript.levelProgress = currentSeaAnimal.levelProgress;
            saveToJson();
        }
    }

    void setGroundItems()
    {
        GameObject itemPrefab;
        GameObject generatedItem;
        foreach (groundItem currentGroundItem in playerData.instance.groundItems)
        {
            if (currentGroundItem.itemSpriteName.Contains("building") == true)
            {
                itemPrefab = Resources.Load<GameObject>("Prefabs/building/" + currentGroundItem.itemSpriteName);
                generatedItem = Instantiate(itemPrefab);
            }
            else
            {
                Sprite itemImage = Resources.Load<Sprite>("decoration/" + currentGroundItem.itemSpriteName);
                itemPrefab = Resources.Load<GameObject>("Prefabs/new Item");
                generatedItem = Instantiate(itemPrefab);
                generatedItem.GetComponent<SpriteRenderer>().sprite = itemImage;
                generatedItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
                generatedItem.AddComponent<BoxCollider2D>();
                generatedItem.GetComponent<BoxCollider2D>().enabled = true;
            }
            generatedItem.GetComponent<Transform>().position = new Vector3(currentGroundItem.x, currentGroundItem.y, 0);
        }
    }

    void setInventory()
    {
        foreach (Item currentItem in playerData.instance.inventory)
        {
            GameObject inventoryCell = Resources.Load<GameObject>("Prefabs/inventory");
            Sprite itemImage = Resources.Load<Sprite>("decoration/" + currentItem.itemSpriteName);
            inventoryCell.GetComponent<Image>().sprite = itemImage;
            GameObject newInventoryCell = Instantiate(inventoryCell, Vector2.zero, Quaternion.identity, inventoryContent.transform);
            inventoryCell inventoryCellScript = newInventoryCell.GetComponent<inventoryCell>();
            playerData.instance.inventoryCells.Add(inventoryCellScript);
        }
    }

    void setSoundSetting()
    {
        setting.instance.bgmNum = playerData.instance.currentBGM;
        setting.instance.setSFXandBGMVolume(playerData.instance.SFXVolume, playerData.instance.BGMVolume);
    }

    void setNewPlayerData()
    {
        playerData.instance.rank = 1;
        playerData.instance.EXP = 0;

        playerData.instance.setMoney(7500);
        playerData.instance.setDiamond(0);
        playerData.instance.setFood(0);

        playerData.instance.currentSeaAnimal = 0;
        playerData.instance.maxSeaAnimal = 8;
        
    }

    public void setPlayerNickname()
    {
        playerData.instance.setPlayerNickname(nicknameInputField.text);
        tutorialCanvas.SetActive(true);
        blackBackground.SetActive(false);
    }
}
