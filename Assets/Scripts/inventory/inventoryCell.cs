using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryCell : MonoBehaviour
{
    public string itemName;
    public string spriteName;
    public int _itemCount;

    public int itemCount
    {
        get
        {
            return _itemCount;
        }
        set
        {
            if (_itemCount != value)
            {
                if (itemCountText == null)
                {
                    itemCountText = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
                }
                itemCountText.text = value.ToString()+"°³";
            }
            _itemCount = value;
        }
    }

    int inventoryIndex;
    public TMP_Text itemNameText;
    public TMP_Text itemCountText;
    public GameObject canvas;
    GameObject decorationOptions;
    GameObject inventoryPanel;
    Button displayButton;

    void Start()
    {
        setVariables();
        displayButton.onClick.AddListener(display);
    }

    void setVariables()
    {
        canvas = GameObject.Find("Canvas");
        decorationOptions = canvas.transform.GetChild(15).gameObject;
        inventoryPanel = transform.parent.parent.parent.parent.gameObject;
        itemNameText = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        itemCountText = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        displayButton = transform.GetChild(2).gameObject.GetComponent<Button>();
        getItemInformationFromPlayerData();
        itemCountText.text = itemCount.ToString()+"°³";
        itemNameText.text = itemName;
    }

    void display()
    {
        getItemInformationFromPlayerData();
        if (itemCount == 1)
        {
            playerData.instance.inventoryCells.RemoveAt(inventoryIndex);
            playerData.instance.inventory.RemoveAt(inventoryIndex);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            itemCount--;
            playerData.instance.inventory[inventoryIndex].itemCount--;
            itemCountText.text = itemCount.ToString() + "°³";
        }
        inventoryPanel.SetActive(false);
        addItemToGround();
        dataManager.instance.saveToJson();
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
            itemPrefab.GetComponent<SpriteRenderer>().sprite = itemImage;
            itemPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
            itemPrefab.AddComponent<BoxCollider2D>();
            generatedItem = Instantiate(itemPrefab);
        }
        decorationManager.instance.setSelectedItem(generatedItem, itemName, spriteName);
        playerData.instance.groundItems.Add(new groundItem(itemName, spriteName, 0f, 0f));
        decorationOptions.SetActive(true);
    }

    void getItemInformationFromPlayerData()
    {
        inventoryIndex = transform.GetSiblingIndex();
        Item inventoryItem = playerData.instance.inventory[inventoryIndex];
        itemName = inventoryItem.itemName;
        spriteName = inventoryItem.itemSpriteName;
        itemCount = inventoryItem.itemCount;
    }
}
