using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class foodProductionManager : MonoBehaviour
{
    public static foodProductionManager instance;
    public GameObject foodProductionPanel;
    public GameObject foodProductionProcess;

    building selectedBuildingScript;
    public foodProductionProcessPanel processScript;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        selectedBuildingScript.currentProduction = selectedItem;
        selectedBuildingScript.productionStartTime = utility.getCurrentUnixTime();
        selectedBuildingScript.productionEndTime = selectedBuildingScript.productionStartTime + selectedItem.timeLong;
        foodProductionPanel.SetActive(false);
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
