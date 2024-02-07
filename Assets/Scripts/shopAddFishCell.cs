using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopAddFishCell : shopCell
{
    int[] fishAddAmounts;
    int[] payCosts;
    int currentFishAddLevel;

    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        buyButton.onClick.AddListener(buyItem);
    }

    public override void buyItem()
    {
        if (canBuy())
        {
            if (paymentType == payment.money)
            {
                playerData.instance.setMoney(playerData.instance.money - itemCost);
            }
            else
            {
                playerData.instance.setDiamond(playerData.instance.diamond - itemCost);
            }
        }
        else
        {
            canNotBuyPanel.SetActive(true);
        }
        
    }
}
