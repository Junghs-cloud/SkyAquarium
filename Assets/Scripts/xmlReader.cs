using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class xmlReader : MonoBehaviour
{
    public static xmlReader instance;
    XmlNodeList itemNodes;
    XmlNodeList fishMoneyNodes;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        loadXML();
    }

    void loadXML()
    {
        TextAsset ItemTextAsset = (TextAsset)Resources.Load("Item");
        TextAsset fishMoneyTextAsset =(TextAsset) Resources.Load("seaAnimal_EarnMoney");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(ItemTextAsset.text);
        itemNodes = xmlDoc.SelectNodes("itemData/item");
        xmlDoc.LoadXml(fishMoneyTextAsset.text);
        fishMoneyNodes = xmlDoc.SelectNodes("seaAnimal/earnMoney");
    }

    public string getItemName(string spriteName)
    {
        string itemName="";
        foreach (XmlNode node in itemNodes)
        {
            if (node.SelectSingleNode("spriteName").InnerText == spriteName)
            {
                itemName = node.SelectSingleNode("itemName").InnerText;
                break;
            }
        }
        return itemName;
    }

    public string getSpriteName(string itemName)
    {
        string spriteName = "";
        foreach (XmlNode node in itemNodes)
        {
            if (node.SelectSingleNode("itemName").InnerText == itemName)
            {
                spriteName = node.SelectSingleNode("spriteName").InnerText;
                break;
            }
        }
        return spriteName;
    }

    public int getSellCost(string spriteName)
    {
        int sellCost = 0;
        foreach (XmlNode node in itemNodes)
        {
            if (node.SelectSingleNode("spriteName").InnerText == spriteName)
            {
                sellCost = int.Parse(node.SelectSingleNode("sellCost").InnerText);
                break;
            }
        }
        return sellCost;
    }

    public float getMoneyPerSec(string seaAnimalName, int level)
    {
        float moneyPerSec = 0;
        foreach (XmlNode node in fishMoneyNodes)
        {
            if (node.SelectSingleNode("seaAnimalName").InnerText == seaAnimalName)
            {
                
                moneyPerSec = float.Parse(node.SelectSingleNode("level"+level).InnerText);
                break;
            }
        }
        return moneyPerSec;
    }

    public int getMaxMoney(string seaAnimalName)
    {
        int maxMoney = 0;
        foreach (XmlNode node in fishMoneyNodes)
        {
            if (node.SelectSingleNode("seaAnimalName").InnerText == seaAnimalName)
            {

                maxMoney = int.Parse(node.SelectSingleNode("maxMoney").InnerText);
                break;
            }
        }
        return maxMoney;
    }
}
