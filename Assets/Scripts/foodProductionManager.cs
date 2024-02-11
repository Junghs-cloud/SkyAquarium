using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class foodProductionManager : MonoBehaviour
{
    public static foodProductionManager instance;
    public GameObject foodProductionPanel;
    public GameObject foodProductionProcess;

    building selectedBuildingScript;
    public foodProductionProcessPanel processScript;

    public GameObject canNotBuyPanel;
    TMP_Text canNotBuyPanelText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        canNotBuyPanelText = canNotBuyPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !decorationManager.instance.isInEditMode())
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f);
            if (isClickedBuilding(hit))
            {
                GameObject selectedBuilding = hit.transform.gameObject;
                selectedBuildingScript = selectedBuilding.GetComponent<building>();
                if (selectedBuildingScript.currentProduction == null)
                {
                    foodProductionPanel.SetActive(true);
                    foodProductionProcess.SetActive(false);
                }
                else if (selectedBuildingScript.isProductionFinished)
                {
                    finishproduction();
                }
                else
                {
                    processScript.StopCoroutine("setTimer");
                    processScript.setTime(selectedBuildingScript.productionStartTime, selectedBuildingScript.productionEndTime);
                    foodProductionProcess.SetActive(true);
                    processScript.StartCoroutine("setTimer");
                }
            }
            else
            {
                foodProductionProcess.SetActive(false);
            }
        }
    }

    bool isClickedBuilding(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.transform.gameObject.tag == "building" && EventSystem.current.IsPointerOverGameObject() == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setCurrentProduction(foodProductionItem selectedItem)
    {
        if (playerData.instance.money < selectedItem.cost)
        {
            canNotBuyPanelText.text = "소지 금액이 부족하여 구매할 수 없습니다.";
            canNotBuyPanel.SetActive(true);
        }
        else
        {
            playerData.instance.setMoney(playerData.instance.money - selectedItem.cost);
            selectedBuildingScript.currentProduction = selectedItem;
            selectedBuildingScript.productionStartTime = utility.getCurrentUnixTime();
            selectedBuildingScript.productionEndTime = selectedBuildingScript.productionStartTime + selectedItem.timeLong;
            foodProductionPanel.SetActive(false);
        }
    }

    void finishproduction()
    {
        foodProductionProcess.SetActive(false);
        int foodProductionAmount = selectedBuildingScript.currentProduction.productionCount;
        playerData.instance.setFood(playerData.instance.food + foodProductionAmount);
        selectedBuildingScript.finishObject.SetActive(false);
        selectedBuildingScript.currentProduction = null;
        selectedBuildingScript.isProductionFinished = false;
    }
}
