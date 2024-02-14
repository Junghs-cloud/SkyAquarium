using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenManager : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject applicationQuitPanel;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (settingPanel.activeSelf == true)
            {
                settingPanel.SetActive(false);
            }
            else if (shopPanel.activeSelf == true)
            {
                shopPanel.SetActive(false);
            }
            else if (inventoryPanel.activeSelf == true)
            {
                inventoryPanel.SetActive(false);
            }
            else if (decorationManager.instance.currentEditType != decorationManager.editType.NULL)
            {
                decorationManager.instance.returnToOriginal();
            }
            else
            {
                applicationQuitPanel.SetActive(true);
            }
        }
    }

    public void quitApplication()
    {
        dataManager.instance.saveToJson();
        Application.Quit();
    }
}
