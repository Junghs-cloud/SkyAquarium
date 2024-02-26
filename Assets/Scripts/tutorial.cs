using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public static tutorial instance;
    public Button settingButton;
    public Button manageButton;
    public Button shopButton;
    public Button buyMoreFishButton;

    public GameObject productionPanel;
    public GameObject shopBuyBlackPanel;
    public GameObject shopSectionBlackPanel;
    public GameObject productionBlackPanel;
    public GameObject fishContentPanel;
    public GameObject buildingContentPanel;

    [Serializable]
    public class tutorialNeed
    {
        public GameObject clickObject;
        public bool useArrowObject;
        public Vector3 arrowObjectPostion;
        public int arrowRotate;
        [TextArea]
        public string tutorialLine;
    };

    [SerializeField]
    public List<tutorialNeed> tutorials;
    public GameObject tutorialArrow;
    public GameObject dialogBox;
    public TMP_Text tutorialLineText;
    public TMP_Text touchScreenText;
    public int currentTutorialLine;

    public Dictionary<int, Vector3> shoudMoveDialogBox;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        shoudMoveDialogBox= new Dictionary<int, Vector3>();
        shoudMoveDialogBox.Add(10, new Vector3(0, 240, 0));
        shoudMoveDialogBox.Add(11, new Vector3(0, -435, 0));
        currentTutorialLine = playerData.instance.currentTutorialLine;
        settingButton.interactable = false;
        manageButton.interactable = false;
        buyMoreFishButton.interactable = false;
        moveToNextTutorial();
        decorationManager.instance.lockButtons();
        productionPanel.GetComponent<ScrollRect>().vertical = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorials[currentTutorialLine].clickObject != null)
            {
                if (EventSystem.current.IsPointerOverGameObject() == true)
                {
                    if (EventSystem.current.currentSelectedGameObject == tutorials[currentTutorialLine].clickObject)
                    {
                        moveToNextTutorial();
                    }
                }
                else
                {
                    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f);
                    if (hit.transform !=null && hit.transform.gameObject == tutorials[currentTutorialLine].clickObject)
                    {
                        moveToNextTutorial();
                    }
                }
            }
            else
            {
                moveToNextTutorial();
            }
        }
    }

    void moveToNextTutorial()
    {
        playerData.instance.currentTutorialLine = currentTutorialLine;
        currentTutorialLine++;
        if (currentTutorialLine == tutorials.Count)
        {
            finishTutorial();
            return;
        }
        tutorialArrow.GetComponent<RectTransform>().anchoredPosition = tutorials[currentTutorialLine].arrowObjectPostion;
        tutorialLineText.text = tutorials[currentTutorialLine].tutorialLine;
        if (tutorials[currentTutorialLine].useArrowObject == false)
        {
            tutorialArrow.SetActive(false);
            touchScreenText.gameObject.SetActive(true);
        }
        else
        {
            tutorialArrow.SetActive(true);
            touchScreenText.gameObject.SetActive(false);
        }
        makeArrowRotate();
        if (shouldControlButtonOnScript())
        {
            controlButtonInteractable();
        }
    }

    void makeArrowRotate()
    {
        tutorialArrow.GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, tutorials[currentTutorialLine].arrowRotate));
    }

    bool shouldControlButtonOnScript()
    {
        if (currentTutorialLine == 7 || currentTutorialLine == 12)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void controlButtonInteractable()
    {
        if (currentTutorialLine == 0 || currentTutorialLine == 7)
        {
            shopButton.interactable = true;
        }
        else
        {
            shopButton.interactable = false;
        }
        if (currentTutorialLine == 1 || currentTutorialLine == 9)
        {
            shopBuyBlackPanel.SetActive(true);
            fishContentPanel.GetComponent<ScrollRect>().horizontal = false;
            buildingContentPanel.GetComponent<ScrollRect>().horizontal = false;
        }
        else
        {
            shopBuyBlackPanel.SetActive(false);
        }
        if (currentTutorialLine == 8)
        {
            shopSectionBlackPanel.SetActive(true);
        }
        else
        {
            shopSectionBlackPanel.SetActive(false);
        }
        if (currentTutorialLine == 12)
        {
            productionBlackPanel.SetActive(true);
        }
        else
        {
            productionBlackPanel.SetActive(false);
        }
        if (currentTutorialLine == 15)
        {
            foodProductionManager.instance.setTutorialLock();
        }
    }

    public void moveDialogBox()
    {
        if (shoudMoveDialogBox.ContainsKey(currentTutorialLine) == true)
        {
            dialogBox.GetComponent<RectTransform>().anchoredPosition = shoudMoveDialogBox[currentTutorialLine];
        }
    }

    public void setClickObject(GameObject gameObject)
    {
        tutorials[currentTutorialLine].clickObject = gameObject;
        touchScreenText.gameObject.SetActive(false);
    }

    public void setClickObject(GameObject gameObject, int lineNum)
    {
        tutorials[lineNum].clickObject = gameObject;
        tutorials[lineNum].arrowObjectPostion = Camera.main.WorldToScreenPoint(gameObject.transform.position+ new Vector3(0, 10f, 0));
        tutorials[lineNum].arrowRotate = -180;
    }

    void finishTutorial()
    {
        this.gameObject.SetActive(false);
        playerData.instance.isTutorialFinished = true;
        decorationManager.instance.unlockButtons();
        settingButton.interactable = true;
        manageButton.interactable = true;
        shopButton.interactable = true;
        buyMoreFishButton.interactable = true;
        fishContentPanel.GetComponent<ScrollRect>().horizontal = true;
        buildingContentPanel.GetComponent<ScrollRect>().horizontal = true;
        foodProductionManager.instance.setTutorialLock();
        dataManager.instance.saveToJson();
    }

}
