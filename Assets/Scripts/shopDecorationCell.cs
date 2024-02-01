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
            canNotBuyPanel.SetActive(true);
        }
    }

    void addItemToGround()
    {
        Sprite itemImage = Resources.Load<Sprite>("decoration/" + spriteName);
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/new Item");
        GameObject generatedItem = Instantiate(itemPrefab);
        generatedItem.GetComponent<SpriteRenderer>().sprite = itemImage;
        generatedItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
        generatedItem.AddComponent<BoxCollider2D>();
        generatedItem.GetComponent<BoxCollider2D>().enabled = true;
        decorationManager.instance.setSelectedItem(generatedItem, itemName);
        
    }
}
