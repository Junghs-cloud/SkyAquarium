using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class shopSeaAnimalCell : shopCell
{
    public GameObject seaAnimalPrefab;
    public int canBuyRank;

    public GameObject blackBackground;
    public GameObject lockObj;

    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        buyButton.onClick.AddListener(buyItem);
        getPaymentType();
        getItemCost();
        getItemName();
        if (playerData.instance.rank >= canBuyRank)
        {
            blackBackground.SetActive(false);
            lockObj.SetActive(false);
        }
    }

    public override void buyItem()
    {
        if (canBuy())
        {
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
            long currentTime = getCurrentUnixTime();
            playerData.instance.setCurrentSeaAnimal(playerData.instance.currentSeaAnimal + 1);
            playerData.instance.marineAnimals.Add(new marineAnimal(instantiatedSeaAnimal.GetInstanceID(), itemName, 1, 0, currentTime));
        }
        else
        {
            canNotBuyPanel.SetActive(true);
        }
    }

    public override bool canBuy()
    {
        if (playerData.instance.currentSeaAnimal == playerData.instance.maxSeaAnimal)
        {
            return false;
        }
        return base.canBuy();
    }

    public long getCurrentUnixTime()
    {
        var now = DateTime.Now.ToLocalTime();
        var span = (now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        long timestamp = (long)span.TotalSeconds;
        return timestamp;
    }
}
