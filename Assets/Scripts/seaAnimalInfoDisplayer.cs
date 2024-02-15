using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class seaAnimalInfoDisplayer : MonoBehaviour
{
    public GameObject statusButton;
    public GameObject statusPanel;

    public TMP_Text seaAnimalName;
    public TMP_Text level;
    public Slider levelProgressBar;
    public TMP_Text levelProgressText;
    public TMP_Text moneyPerSec;
    public TMP_Text maxMoney;
    public TMP_Text foodAmount;

    public Button collectButton;
    public Button feedButton;
    public Button sellButton;
    seaAnimal currentClickAnimal;
    marineAnimal currentClickMarineAnimal;

    void Start()
    {
        collectButton.onClick.AddListener(collect);
        feedButton.onClick.AddListener(feed);
        sellButton.onClick.AddListener(sell);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f);
            if (!decorationManager.instance.isInEditMode() && hit.collider != null && hit.transform.gameObject.tag == "seaAnimal")
            {
                currentClickAnimal = hit.transform.gameObject.GetComponent<seaAnimal>();
                currentClickMarineAnimal = playerData.instance.marineAnimals.Find(seaAnimal => seaAnimal.instanceID == currentClickAnimal.gameObject.GetInstanceID());
                seaAnimalName.text = currentClickAnimal.seaAnimalName;
                updateLevelInformation();
                updateMoneyAndFoodInformation();
                collectButton.gameObject.SetActive(true);
                statusButton.SetActive(true);
            }
            else
            {
                statusPanel.SetActive(false);
                collectButton.gameObject.SetActive(false);
                statusButton.SetActive(false);
            }
        }
    }

    void collect()
    {
        long currentUnixTime = utility.getCurrentUnixTime();
        long lastCollectTime = currentClickMarineAnimal.lastCollect;
        long earnMoney = (long) ((currentUnixTime - lastCollectTime) * currentClickAnimal.moneyPerSec);
        if (earnMoney > currentClickAnimal.maxMoney)
        {
            earnMoney = currentClickAnimal.maxMoney;
        }
        if (earnMoney != 0)
        {
            currentClickMarineAnimal.lastCollect = currentUnixTime;
            playerData.instance.setMoney(playerData.instance.money + (int)earnMoney);
            dataManager.instance.saveToJson();
            setting.instance.playSFX(setting.sfx.coin);
        }
    }

    void feed()
    {
        if (currentClickAnimal.level == 10 && currentClickAnimal.levelProgress == 5)
        {
            return;
        }
        currentClickAnimal.levelProgress++;
        playerData.instance.setFood(playerData.instance.food - currentClickAnimal.foodAmount);
        if (currentClickAnimal.levelProgress == 5)
        {
            if (currentClickAnimal.level != 10)
            {
                currentClickAnimal.levelProgress = 0;
                currentClickAnimal.level++;
                updateMoneyAndFoodInformation();
            }
        }
        updateLevelInformation();
        dataManager.instance.saveToJson();
    }

    void sell()
    {
        playerData.instance.marineAnimals.Remove(currentClickMarineAnimal);
        dataManager.instance.saveToJson();
        playerData.instance.setCurrentSeaAnimal(playerData.instance.currentSeaAnimal - 1);
        playerData.instance.setMoney(playerData.instance.money + currentClickAnimal.sellCost);
        playerData.instance.setEXP(playerData.instance.EXP + currentClickAnimal.sellEXP);
        statusPanel.SetActive(false);
        collectButton.gameObject.SetActive(false);
        statusButton.SetActive(false);
        GameObject.Destroy(currentClickAnimal.gameObject);
    }

    void updateMoneyAndFoodInformation()
    {
        currentClickAnimal.getCurrentMoneyAndFoodInfo();
        moneyPerSec.text = currentClickAnimal.moneyPerSec.ToString();
        maxMoney.text=currentClickAnimal.maxMoney.ToString();
        foodAmount.text=currentClickAnimal.foodAmount.ToString()+"°³ ¼Òºñ";
    }

    void updateLevelInformation()
    {
        Debug.Log(currentClickAnimal+"  "+currentClickMarineAnimal);
        levelProgressText.text = currentClickAnimal.levelProgress.ToString() + " / 5";
        levelProgressBar.value = (float) currentClickAnimal.levelProgress / 5f;
        level.text = "Lv. " + currentClickAnimal.level.ToString();
        currentClickMarineAnimal.level = currentClickAnimal.level;
        currentClickMarineAnimal.levelProgress = currentClickAnimal.levelProgress;
    }

}
