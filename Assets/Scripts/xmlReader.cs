using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class xmlReader : MonoBehaviour
{
    public static xmlReader instance;
    XmlNodeList itemNodes;
    XmlNodeList fishMoneyNodes;
    XmlNodeList fishFoodNodes;
    XmlNodeList fishInformationNodes;
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
        XmlDocument xmlDoc1 = new XmlDocument();
        XmlDocument xmlDoc2 = new XmlDocument();
        xmlDoc1.LoadXml(ItemTextAsset.text);
        itemNodes = xmlDoc1.SelectNodes("itemData/item");
        xmlDoc2.LoadXml(fishMoneyTextAsset.text);
        fishMoneyNodes = xmlDoc2.SelectNodes("seaAnimal/earnMoney");
        fishFoodNodes = xmlDoc2.SelectNodes("seaAnimal/food");
        fishInformationNodes = xmlDoc2.SelectNodes("seaAnimal/information");
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

    public int getFoodAmount(string seaAnimalName, int level)
    {
        int levelFood = 0;
        foreach (XmlNode node in fishFoodNodes)
        {
            if (node.SelectSingleNode("seaAnimalName").InnerText == seaAnimalName)
            {

                levelFood = int.Parse(node.SelectSingleNode("level" + level).InnerText);
                break;
            }
        }
        return levelFood;
    }

    public int getSeaAnimalSellCost(string seaAnimalName)
    {
        int sellCost = 0;
        foreach (XmlNode node in fishInformationNodes)
        {
            if (node.SelectSingleNode("seaAnimalName").InnerText == seaAnimalName)
            {
                sellCost = int.Parse(node.SelectSingleNode("sellCost").InnerText);
                break;
            }
        }
        return sellCost;
    }

    public int getSellEXP(string seaAnimalName)
    {
        int sellEXP = 0;
        foreach (XmlNode node in fishInformationNodes)
        {
            if (node.SelectSingleNode("seaAnimalName").InnerText == seaAnimalName)
            {

                sellEXP = int.Parse(node.SelectSingleNode("sellEXP").InnerText);
                break;
            }
        }
        return sellEXP;
    }
}
