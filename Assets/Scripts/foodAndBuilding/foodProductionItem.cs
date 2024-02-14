using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class foodProductionItem : MonoBehaviour
{
    public string itemName;
    public string timeStr;

    public int productionCount;
    public int cost;

    public TMP_Text itemNameText;
    public TMP_Text timeText;
    public TMP_Text productionCountText;
    public TMP_Text costText;

    [HideInInspector]
    public long timeLong;
    void Start()
    {
        itemNameText.text = itemName;
        utility.addCommaToTMPTexts(productionCount, productionCountText);
        utility.addCommaToTMPTexts(cost, costText);
        timeText.text = timeStr.ToString();
        setTimeLong();
    }

    void setTimeLong()
    {
        int hour = int.Parse(timeStr.Substring(0, 2));
        int min = int.Parse(timeStr.Substring(3,2));
        int sec = int.Parse(timeStr.Substring(6,2));
        timeLong = hour * 3600 + min * 60 + sec;
    }

}
