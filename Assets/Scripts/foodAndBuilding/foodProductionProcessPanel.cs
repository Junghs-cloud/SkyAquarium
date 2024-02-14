using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class foodProductionProcessPanel : MonoBehaviour
{
    public TMP_Text timeText;
    public Slider processSlider;
    public long startTime;
    public long endTime;

    long currentUnixTime;
    string leftTimeString;

    IEnumerator setTimer()
    {
        currentUnixTime = utility.getCurrentUnixTime();
        setProcessSliderValue();
        while (currentUnixTime < endTime)
        {
            setProcessSliderValue();
            yield return new WaitForSeconds(1.0f);
            currentUnixTime++;
            setProcessSliderValue();
        }
    }

    public void setTime(long startTime, long endTime)
    {
        this.startTime = startTime;
        this.endTime = endTime;
    }

    void setProcessSliderValue()
    {
        long denominator = endTime - startTime;
        long numerator = currentUnixTime - startTime;
        long leftTime = denominator - numerator;
        processSlider.value = (float) numerator / (float) denominator;
        convertTimeFormat(leftTime);
    }

    void convertTimeFormat(long leftTime)
    {
        long hour = leftTime / 3600;
        long left = leftTime % 3600;
        long min = left / 60;
        left= left % 60;
        long sec = left;
        leftTimeString = "";
        makeTimeString(hour);
        leftTimeString += ":";
        makeTimeString(min);
        leftTimeString += ":";
        makeTimeString(sec);
        timeText.text = leftTimeString;
    }

    void makeTimeString(long factor)
    {
        if (factor <= 9)
        {
            leftTimeString += "0";
        }
        leftTimeString += factor.ToString();
    }
}
