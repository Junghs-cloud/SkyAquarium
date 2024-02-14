using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class building : MonoBehaviour
{
    public foodProductionItem currentProduction;
    public long productionStartTime;
    public long productionEndTime;

    public GameObject finishObject;
    public bool isProductionFinished;

    void Start()
    {
        currentProduction = null;
        finishObject = transform.GetChild(0).gameObject;
        finishObject.SetActive(false);
    }

    void Update()
    {
        if (currentProduction != null)
        {
            long currentUnixTime = utility.getCurrentUnixTime();
            if (currentUnixTime >= productionEndTime)
            {
                finishObject.SetActive(true);
                isProductionFinished = true;
            }
        }
    }

}
