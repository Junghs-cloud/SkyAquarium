using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canNotBuyPanel : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine("waitAndDisable");
    }

    IEnumerator waitAndDisable()
    {
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }
}
