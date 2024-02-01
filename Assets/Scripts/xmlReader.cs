using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class xmlReader : MonoBehaviour
{
    public static xmlReader instance;
    XmlNodeList nodes;
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
        TextAsset textAsset = (TextAsset)Resources.Load("Item");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        nodes = xmlDoc.SelectNodes("itemData/item");
        foreach (XmlNode node in nodes)
        {
            Debug.Log(node.SelectSingleNode("itemName").InnerText);
        }
    }

    public string getItemName(string spriteName)
    {
        string itemName="";
        foreach (XmlNode node in nodes)
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
        foreach (XmlNode node in nodes)
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
        foreach (XmlNode node in nodes)
        {
            if (node.SelectSingleNode("spriteName").InnerText == spriteName)
            {
                sellCost = int.Parse(node.SelectSingleNode("sellCost").InnerText);
                break;
            }
        }
        return sellCost;
    }
}
