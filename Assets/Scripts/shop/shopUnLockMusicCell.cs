using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopUnLockMusicCell : shopCell
{
    private int unlockBGMNum;
    private TMP_Text itemNameText;
    private GameObject soldOutImage;

    void Start()
    {
        setObjectsFromTransform();
        buyButton.onClick.AddListener(buyItem);
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        getPaymentType();
        getItemCost();
    }

    void setObjectsFromTransform()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        soldOutImage = transform.GetChild(3).gameObject;
        itemNameText = transform.GetChild(2).GetComponent<TMP_Text>();
        char numChar = itemNameText.text[itemNameText.text.Length - 1];
        unlockBGMNum = int.Parse(numChar.ToString());
    }

    public override void buyItem()
    {
        if (canBuy())
        {
            playerData.instance.musicUnLock[unlockBGMNum + 3] = true;
            playerData.instance.setMoney(playerData.instance.money - itemCost);
            soldOutImage.SetActive(true);
            setting.instance.playSFX(setting.sfx.coin);
        }
        else
        {
            canNotBuyPanelText.text = "소지 금액이 부족하여 구매할 수 없습니다..";
            canNotBuyPanel.SetActive(true);
        }
    }
}
