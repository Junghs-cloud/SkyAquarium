using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopDecorationCell : shopCell
{
    public GameObject decorationOptions;
    public string spriteName;
    void Start()
    {
        buyButton = transform.GetChild(0).GetComponent<Button>();
        spriteName = transform.GetChild(1).GetComponent<Image>().sprite.name;
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
            addItemToGround();
            decorationOptions.SetActive(true);
            decorationManager.instance.setBuyingItemCost(itemCost);
            decorationManager.instance.currentEditType = decorationManager.editType.buy;
            shopPanel.SetActive(false);
        }
        else
        {
            canNotBuyPanelText.text = "소지 금액이 부족하여 구매할 수 없습니다.";
            canNotBuyPanel.SetActive(true);
        }
    }

    void addItemToGround()
    {
        GameObject itemPrefab;
        GameObject generatedItem;
        if (spriteName.Contains("building") == true)
        {
            itemPrefab = Resources.Load<GameObject>("Prefabs/building/" + spriteName);
            generatedItem = Instantiate(itemPrefab);
        }
        else
        {
            Sprite itemImage = Resources.Load<Sprite>("decoration/" + spriteName);
            itemPrefab = Resources.Load<GameObject>("Prefabs/new Item");
            generatedItem = Instantiate(itemPrefab);
            generatedItem.GetComponent<SpriteRenderer>().sprite = itemImage;
            generatedItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
            generatedItem.AddComponent<BoxCollider2D>();
            generatedItem.GetComponent<BoxCollider2D>().enabled = true;
        }
        decorationManager.instance.setSelectedItem(generatedItem, itemName, spriteName);
        
    }
}
