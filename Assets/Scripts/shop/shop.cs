using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shop : MonoBehaviour
{
    public GameObject fishPanel;
    public GameObject decorationPanel;
    public GameObject buildingPanel;
    public GameObject etcPanel;

    public Button fishButton;
    public Button decorationButton;
    public Button buildingButton;
    public Button etcButton;

    public TMP_Text sectionInfoText;
    public AudioSource SFXPlayer;

    void Start()
    {
        fishButton.onClick.AddListener(setButtonsPostion1);
        buildingButton.onClick.AddListener(setButtonsPostion2);
        decorationButton.onClick.AddListener(setButtonsPostion3);
        etcButton.onClick.AddListener(setButtonsPosition4);
        SFXPlayer.playOnAwake = false;
    }

    void OnDisable()
    {
        setButtonsPostion1();
    }

    void setButtonsPostion1()
    {
        fishPanel.SetActive(true);
        decorationPanel.SetActive(false);
        buildingPanel.SetActive(false);
        etcPanel.SetActive(false);

        sectionInfoText.text = "해양생물";

        makeButtonBig(fishButton, 360);
        makeButtonSmall(buildingButton, 270);
        makeButtonSmall(decorationButton, 180);
        makeButtonSmall(etcButton, 90);
    }

    void setButtonsPostion2()
    {
        fishPanel.SetActive(false);
        buildingPanel.SetActive(true);
        decorationPanel.SetActive(false);
        etcPanel.SetActive(false);

        sectionInfoText.text = "건축물";

        makeButtonSmall(fishButton, 360);
        makeButtonBig(buildingButton, 270);
        makeButtonSmall(decorationButton, 180);
        makeButtonSmall(etcButton, 90);
    }

    void setButtonsPostion3()
    {
        fishPanel.SetActive(false);
        buildingPanel.SetActive(false);
        decorationPanel.SetActive(true);
        etcPanel.SetActive(false);

        sectionInfoText.text = "데코레이션";

        makeButtonSmall(fishButton, 360);
        makeButtonSmall(buildingButton, 270);
        makeButtonBig(decorationButton, 180);
        makeButtonSmall(etcButton, 90);
    }

    void setButtonsPosition4()
    {
        fishPanel.SetActive(false);
        buildingPanel.SetActive(false);
        decorationPanel.SetActive(false);
        etcPanel.SetActive(true);

        sectionInfoText.text = "기타";

        makeButtonSmall(fishButton, 360);
        makeButtonSmall(buildingButton, 270);
        makeButtonSmall(decorationButton, 180);
        makeButtonBig(etcButton, 90);
    }

    void makeButtonBig(Button button, int posY)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-870, posY);
        rectTransform.sizeDelta = new Vector2(160, 60);
    }

    void makeButtonSmall(Button button, int posY)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-830, posY);
        rectTransform.sizeDelta = new Vector2(80, 60);
    }

}

public class shopCell : MonoBehaviour
{
    [HideInInspector]
    public enum payment { money, diamond };
    public payment paymentType;
    public Button buyButton;
    public string itemName;
    public int itemCost;

    [Header("-shoud Add In Inspector-")]
    public GameObject canNotBuyPanel;
    public GameObject shopPanel;

    protected TMP_Text canNotBuyPanelText;

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
