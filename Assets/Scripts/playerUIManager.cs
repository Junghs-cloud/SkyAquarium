using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerUIManager : MonoBehaviour
{
    public static playerUIManager instance;
    public TMP_Text moneyTMP;
    public TMP_Text diamondTMP;
    public TMP_Text foodTMP;

    public TMP_Text currentSeaAnimalTMP;
    public TMP_Text maxSeaAnimalTMP;

    Stack stringStack;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        stringStack = new Stack();
        updatePlayerAssetTMP();
    }

    public void updatePlayerAssetTMP()
    {
        addCommaToTMPTexts(playerData.instance.money, moneyTMP);
        addCommaToTMPTexts(playerData.instance.diamond, diamondTMP);
        addCommaToTMPTexts(playerData.instance.food, foodTMP);
    }

    void addCommaToTMPTexts(int asset, TMP_Text assetText)
    {
        assetText.text = "";
        string assetString = asset.ToString();
        for (int i = 0; i < assetString.Length; i++)
        {
            stringStack.Push(assetString[assetString.Length - i - 1]);
            if (i != assetString.Length - 1 && i % 3 == 2)
            {
                stringStack.Push(',');
            }
        }
        while (stringStack.Count != 0)
        {
            assetText.text += stringStack.Peek();
            stringStack.Pop();
        }
    }

    public void updateCurrentSeaAnimalTMP()
    {
        currentSeaAnimalTMP.text = playerData.instance.currentSeaAnimal.ToString();
    }
}
