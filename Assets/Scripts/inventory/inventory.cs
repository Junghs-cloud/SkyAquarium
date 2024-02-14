using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public GameObject hasItemObject;
    public GameObject noItemObject;

    void OnEnable()
    {
        if (playerData.instance.inventory.Count == 0)
        {
            noItemObject.SetActive(true);
            hasItemObject.SetActive(false);
        }
        else
        {
            hasItemObject.SetActive(true);
            noItemObject.SetActive(false);
        }
    }
}
