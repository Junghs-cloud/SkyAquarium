using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class decorationManager : MonoBehaviour
{
    public static decorationManager instance;
    public GameObject inventoryButton;
    public GameObject decorationOptions;

    GameObject selectedItem;
    Vector3 selectedItemOriginalPosition;
    string selectedItemName;
    string selectedItemSpriteName;

    public Button manageButton;
    public Button cancelButton;
    public Button storeButton;
    public Button sellButton;
    public Button confirmButton;
    public Button editModeConfirmButton;

    public GameObject inventoryPanel;
    public GameObject inventoryContent;
    List<Item> inventory;

    public enum editType {NULL, edit, buy };
    public editType currentEditType;

    int buyingItemCost;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        addListenersToButtons();
        selectedItem = null;
        currentEditType = editType.NULL;
        inventory = playerData.instance.inventory;
    }

    void addListenersToButtons()
    {
        manageButton.onClick.AddListener(setCurrentEditTypeToEdit);
        cancelButton.onClick.AddListener(cancel);
        storeButton.onClick.AddListener(store);
        sellButton.onClick.AddListener(sell);
        confirmButton.onClick.AddListener(confirm);
        editModeConfirmButton.onClick.AddListener(returnToOriginal);
    }

    void Update()
    {
        if (isInEditMode())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isObjectSelected())
                {
                    editObject();
                }
                else
                {
                    selectObject();
                }
            }
        }
    }

    public bool isInEditMode()
    {
        if (currentEditType  != editType.NULL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isObjectSelected()
    {
        if (decorationOptions.activeSelf == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void editObject()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newObjectPosition = new Vector3(clickPosition.x, clickPosition.y, 0);
            selectedItem.transform.position = newObjectPosition;
        }
    }

    void selectObject()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f);
        if (hit.collider != null && hit.transform.gameObject.tag == "decoration" && EventSystem.current.IsPointerOverGameObject() == false)
        {
            selectedItem = hit.transform.gameObject;
            selectedItemSpriteName = selectedItem.GetComponent<SpriteRenderer>().sprite.name;
            selectedItemName = xmlReader.instance.getItemName(selectedItemSpriteName);
            selectedItemOriginalPosition = selectedItem.transform.position;
            decorationOptions.SetActive(true);
        }
    }

    void cancel()
    {
        if (currentEditType == editType.edit)
        {
            selectedItem.transform.position = selectedItemOriginalPosition;
            selectedItem = null;
            decorationOptions.SetActive(false);
        }
        else if (currentEditType == editType.buy)
        {
            GameObject.Destroy(selectedItem);
            selectedItem = null;
            currentEditType = editType.NULL;
        }
    }

    void store()
    {
        //상점에서 구매시 구매 금액만큼 빠져나가게 하기 구현
        int inventorySameItemIndex = inventory.FindIndex(item => item.itemSpriteName == selectedItemSpriteName);
        if (inventorySameItemIndex == -1)
        {
            addItemToPlayerData();
        }
        else
        {
            int currentItemCount = ++inventory[inventorySameItemIndex].itemCount;
            playerData.instance.inventoryCells[inventorySameItemIndex].itemCount = currentItemCount;
        }
        groundItem selectedGroundItem = playerData.instance.groundItems.Find(item => item.itemName == selectedItemName && item.x == selectedItemOriginalPosition.x && item.y== selectedItemOriginalPosition.y);
        playerData.instance.groundItems.Remove(selectedGroundItem);
        Debug.Log(playerData.instance.groundItems.Count);
        GameObject.Destroy(selectedItem);
        selectedItem = null;
        decorationOptions.SetActive(false);
    }

    void sell()
    {
        if (currentEditType == editType.buy)
        {
            return;
        }
        int sellCost = xmlReader.instance.getSellCost(selectedItemSpriteName);
        groundItem soldItem = playerData.instance.groundItems.Find(item => item.x == selectedItemOriginalPosition.x && item.y == selectedItemOriginalPosition.y);
        playerData.instance.groundItems.Remove(soldItem);
        playerData.instance.setMoney(playerData.instance.money + sellCost);
        GameObject.Destroy(selectedItem);
        selectedItem = null;
        decorationOptions.SetActive(false);
    }

    void confirm()
    {
        Vector3 selectedItemPosition = selectedItem.transform.position;
        if (currentEditType == editType.edit)
        {
            Debug.Log(selectedItemName);
            int groundItemSameItemIndex = playerData.instance.groundItems.FindIndex(item => item.itemName == selectedItemName && item.x == selectedItemOriginalPosition.x && item.y == selectedItemOriginalPosition.y);
            Debug.Log(groundItemSameItemIndex);
            playerData.instance.groundItems[groundItemSameItemIndex].x = selectedItemPosition.x;
            playerData.instance.groundItems[groundItemSameItemIndex].y = selectedItemPosition.y;
        }
        else if (currentEditType == editType.buy)
        {
            playerData.instance.groundItems.Add(new groundItem(selectedItemName, selectedItemSpriteName, selectedItemPosition.x, selectedItemPosition.y));
            playerData.instance.setMoney(playerData.instance.money - buyingItemCost);
            currentEditType = editType.NULL;
        }
        selectedItem = null;
        decorationOptions.SetActive(false);
    }

    void addItemToPlayerData()
    {
        GameObject inventoryCell = Resources.Load<GameObject>("Prefabs/inventory");
        Sprite itemImage = Resources.Load<Sprite>("decoration/" + selectedItemSpriteName);
        inventoryCell.GetComponent<Image>().sprite = itemImage;
        GameObject newInventoryCell = Instantiate(inventoryCell, Vector2.zero, Quaternion.identity, inventoryContent.transform);
        inventoryCell inventoryCellScript = newInventoryCell.GetComponent<inventoryCell>();
        inventory.Add(new Item(selectedItemName, selectedItemSpriteName, 1));
        playerData.instance.inventoryCells.Add(inventoryCellScript);
    }

    public void setSelectedItem(GameObject selectedItem, string itemName)
    {
        this.selectedItem = selectedItem;
        selectedItemName = itemName;
        selectedItemOriginalPosition = Vector3.zero;
    }

    void setCurrentEditTypeToEdit()
    {
        currentEditType = editType.edit;
    }

    void returnToOriginal()
    {
        if (decorationOptions.activeSelf == true)
        {
            return;
        }
        else
        {
            currentEditType = editType.NULL;
            editModeConfirmButton.gameObject.SetActive(false);
            inventoryButton.gameObject.SetActive(false);
            inventoryPanel.gameObject.SetActive(false);
        }
    }

    public void setBuyingItemCost(int itemCost)
    {
        buyingItemCost = itemCost;
    }
}
