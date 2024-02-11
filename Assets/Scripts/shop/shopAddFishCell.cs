using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopAddFishCell : shopCell
{
    public GameObject soldOut;
    public int[] fishMaxAmounts;
    public int[] payCosts;
   
    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        buyButton.onClick.AddListener(buyItem);
        getPaymentType();
        getItemCost();
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
            playerData.instance.setMaxSeaAnimal(fishMaxAmounts[playerData.instance.fishAmountLevel]);
            RaiseFishAmountLevel();
        }
        else
        {
            canNotBuyPanelText.text = "���� �ݾ��� �����Ͽ� ������ �� �����ϴ�.";
            canNotBuyPanel.SetActive(true);
        }   
    }

    public void RaiseFishAmountLevel()
    {
        int currentFishLevel = ++playerData.instance.fishAmountLevel;
        TMP_Text costText = buyButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        if (currentFishLevel == fishMaxAmounts.Length)
        {
            soldOut.SetActive(true);
            return;
        }
        utility.addCommaToTMPTexts(payCosts[currentFishLevel], costText);
        getItemCost();
    }
}
