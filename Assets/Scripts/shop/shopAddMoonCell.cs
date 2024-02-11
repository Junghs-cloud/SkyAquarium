using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopAddMoonCell : shopCell
{
    public GameObject moonImage;
    private GameObject soldOutImage;

    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        buyButton.onClick.AddListener(buyItem);
        soldOutImage = transform.GetChild(3).gameObject;
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
            playerData.instance.hasMoonBackground = true;
            moonImage.SetActive(true);
            soldOutImage.SetActive(true);
        }
        else
        {
            canNotBuyPanelText.text = "소지 금액이 부족하여 구매할 수 없습니다.";
            canNotBuyPanel.SetActive(true);
        }

    }
}
