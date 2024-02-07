using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class utility : MonoBehaviour
{

    public static long getCurrentUnixTime()
    {
        var now = DateTime.Now.ToLocalTime();
        var span = (now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        long timestamp = (long)span.TotalSeconds;
        return timestamp;
    }

    public static void addCommaToTMPTexts(int asset, TMP_Text assetText)
    {
        Stack stringStack = new Stack();
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

}
