using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shop : MonoBehaviour
{
    public GameObject fishPanel;
    public GameObject decorationPanel;
    public GameObject farmPanel;

    public Button fishButton;
    public Button decorationButton;
    public Button farmButton;

    public TMP_Text sectionInfoText;
    void Start()
    {
        fishButton.onClick.AddListener(setButtonsPostion1);
        decorationButton.onClick.AddListener(setButtonsPostion2);
    }

    void setButtonsPostion1()
    {
        decorationPanel.SetActive(false);
        farmPanel.SetActive(false);

        sectionInfoText.text = "해양생물";

        RectTransform rectTransform = fishButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-870, 360);
        rectTransform.sizeDelta= new Vector2(160, 60);

        rectTransform = decorationButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-830, 180);
        rectTransform.sizeDelta = new Vector2(80, 60);
    }

    void setButtonsPostion2()
    {
        fishPanel.SetActive(false);
        farmPanel.SetActive(false);

        sectionInfoText.text = "데코레이션";

        RectTransform rectTransform = fishButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-830, 360);
        rectTransform.sizeDelta = new Vector2(80, 60);

        rectTransform = decorationButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-870, 180);
        rectTransform.sizeDelta = new Vector2(160, 60);
    }
}

public class shopCell : MonoBehaviour
{
    public enum payment { money, diamond };
    public payment paymentType;
    public Button buyButton;
    public GameObject canNotBuyPanel;
    public string itemName;
    public int itemCost;
    public GameObject shopPanel;

    public void getPaymentType()
    {
        if (buyButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name == "money")
        {
            paymentType = payment.money;
        }
        else
        {
            paymentType = payment.diamond;
        }
    }

    public void getItemName()
    {
        itemName = transform.GetChild(2).GetComponent<TMP_Text>().text;
    }

    public void getItemCost()
    {
        string tempItemCostString = buyButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text;
        string itemCostString = "";
        for (int i = 0; i < tempItemCostString.Length; i++)
        {
            if (tempItemCostString[i] != ',')
            {
                itemCostString += tempItemCostString[i];
            }
        }
        itemCost = int.Parse(itemCostString);
    }

    public virtual bool canBuy()
    {
        if (paymentType == payment.money && playerData.instance.money >= itemCost)
        {
            return true;
        }
        else if (paymentType == payment.diamond && playerData.instance.diamond >= itemCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void buyItem()
    {
        if (canBuy())
        {
            Debug.Log("부모-살 수 있음");
        }
        else
        {
            Debug.Log("구매 불가");
        }
    }

}
