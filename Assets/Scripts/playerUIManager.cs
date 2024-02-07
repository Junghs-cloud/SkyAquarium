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

    public TMP_Text nicknameTMP;
    public TMP_Text rankTMP;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        updatePlayerAssetTMP();
    }

    public void updatePlayerAssetTMP()
    {
        utility.addCommaToTMPTexts(playerData.instance.money, moneyTMP);
        utility.addCommaToTMPTexts(playerData.instance.diamond, diamondTMP);
        utility.addCommaToTMPTexts(playerData.instance.food, foodTMP);
        nicknameTMP.text = playerData.instance.nickname;
        rankTMP.text = "RANK " + playerData.instance.rank.ToString();
    }

    public void updateCurrentSeaAnimalTMP()
    {
        currentSeaAnimalTMP.text = playerData.instance.currentSeaAnimal.ToString();
    }
}
