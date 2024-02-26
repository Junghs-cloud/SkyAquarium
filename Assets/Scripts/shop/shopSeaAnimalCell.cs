using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class shopSeaAnimalCell : shopCell
{
    public GameObject seaAnimalPrefab;
    string spriteName;

    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        spriteName = transform.GetChild(1).GetComponent<Image>().sprite.name;
        buyButton.onClick.AddListener(buyItem);
        getPaymentType();
        getItemCost();
        getItemName();
    }

    public override void buyItem()
    {
        if (canBuy())
        {
            if (hasMaxSeaAnimal())
            {
                canNotBuyPanelText.text = "최대 물고기 보유 수를 초과하여 해양생물을 구매할 수 없습니다.";
                canNotBuyPanel.SetActive(true);
            }
            else
            {
                buySeaAnimal();
            }
        }
        else
        {
            canNotBuyPanelText.text = "소지 금액이 부족하여 구매할 수 없습니다.";
            canNotBuyPanel.SetActive(true);
        }
    }

    bool hasMaxSeaAnimal()
    {
        if (playerData.instance.currentSeaAnimal == playerData.instance.maxSeaAnimal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void buySeaAnimal()
    {
        setting.instance.playSFX(setting.sfx.fishSplash);
        shopPanel.SetActive(false);
        GameObject instantiatedSeaAnimal = Instantiate(seaAnimalPrefab);
        if (paymentType == payment.money)
        {
            playerData.instance.setMoney(playerData.instance.money - itemCost);
        }
        else
        {
            playerData.instance.setDiamond(playerData.instance.diamond - itemCost);
        }
        long currentTime = utility.getCurrentUnixTime();
        playerData.instance.setCurrentSeaAnimal(playerData.instance.currentSeaAnimal + 1);
        playerData.instance.marineAnimals.Add(new marineAnimal(instantiatedSeaAnimal.GetInstanceID(), itemName, spriteName, 1, 0, currentTime));
        dataManager.instance.saveToJson();
        if (playerData.instance.isTutorialFinished == false)
        {
            tutorial.instance.setClickObject(instantiatedSeaAnimal);
        }
    }
}
