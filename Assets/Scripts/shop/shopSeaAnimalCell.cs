using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class shopSeaAnimalCell : shopCell
{
    public GameObject seaAnimalPrefab;

    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        buyButton.onClick.AddListener(buyItem);
        getPaymentType();
        getItemCost();
        getItemName();
    }

    public override void buyItem()
    {
        if (canBuy())
        {
            if (playerData.instance.currentSeaAnimal == playerData.instance.maxSeaAnimal)
            {
                canNotBuyPanelText.text = "�ִ� ����� ���� ���� �ʰ��Ͽ� �ؾ������ ������ �� �����ϴ�.";
                canNotBuyPanel.SetActive(true);
            }
            else
            {
                buySeaAnimal();
            }
        }
        else
        {
            canNotBuyPanelText.text = "���� �ݾ��� �����Ͽ� ������ �� �����ϴ�.";
            canNotBuyPanel.SetActive(true);
        }
    }

    void buySeaAnimal()
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
        long currentTime = utility.getCurrentUnixTime();
        playerData.instance.setCurrentSeaAnimal(playerData.instance.currentSeaAnimal + 1);
        playerData.instance.marineAnimals.Add(new marineAnimal(instantiatedSeaAnimal.GetInstanceID(), itemName, 1, 0, currentTime));
    }
}
