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
                updateMoneyInformation();
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
            Debug.Log(earnMoney);
            currentClickMarineAnimal.lastCollect = currentUnixTime;
            playerData.instance.setMoney(playerData.instance.money + (int)earnMoney);
        }
    }

    void feed()
    {
        if (currentClickAnimal.level == 5 && currentClickAnimal.levelProgress == 5)
        {
            return;
        }
        currentClickAnimal.levelProgress++;
        if (currentClickAnimal.levelProgress == 5)
        {
            if (currentClickAnimal.level != 5)
            {
                currentClickAnimal.levelProgress = 0;
                currentClickAnimal.level++;
                updateMoneyInformation();
            }
        }
        updateLevelInformation();
    }

    void sell()
    {
        playerData.instance.marineAnimals.Remove(currentClickMarineAnimal);
        GameObject.Destroy(currentClickAnimal.gameObject);
        playerData.instance.setCurrentSeaAnimal(playerData.instance.currentSeaAnimal - 1);
        statusPanel.SetActive(false);
        collectButton.gameObject.SetActive(false);
        statusButton.SetActive(false);
    }

    void updateMoneyInformation()
    {
        currentClickAnimal.getCurrentMoneyAndFoodInfo();
        moneyPerSec.text = currentClickAnimal.moneyPerSec.ToString();
    }

    void updateLevelInformation()
    {
        levelProgressText.text = currentClickAnimal.levelProgress.ToString() + " / 5";
        levelProgressBar.value = (float) currentClickAnimal.levelProgress / 5f;
        level.text = "Lv. " + currentClickAnimal.level.ToString();
        currentClickMarineAnimal.level = currentClickAnimal.level;
        currentClickMarineAnimal.levelProgress = currentClickAnimal.levelProgress;
    }

}
