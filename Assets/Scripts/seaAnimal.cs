using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class seaAnimal : MonoBehaviour
{
    Vector3 position;
   Vector3 targetpos;

    private bool flip;
    public float speed = 1;
    const float errorRange= 0.05f;

    [Header("Status")]
    public string seaAnimalName;
    public int level;
    public int levelProgress;

    public float moneyPerSec;
    public int foodAmount;
    public int maxMoney;

    public int sellCost;
    public int sellEXP;

    void Start()
    {
        float x = Random.Range(-28, 28);
        float y = Random.Range(-10f, 12f);
        position = new Vector3(x, y, 0);
        transform.position = position;
        getNewTargetPos();
        sellCost = xmlReader.instance.getSeaAnimalSellCost(seaAnimalName);
        sellEXP = xmlReader.instance.getSellEXP(seaAnimalName);
    }

    void Update()
    {
        setImageDirection();
        move();
        if (isInErrorRange())
        {
            getNewTargetPos();
        }

    }

    void setImageDirection()
    {
        if (flip == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void move()
    {
        if (position.x >= targetpos.x)
        {
            position.x = position.x + speed * (-1) * Time.deltaTime;
            flip = true;
        }
        else if (position.x < targetpos.x)
        {
            position.x = position.x + speed * 1 * Time.deltaTime;
            flip = false;
        }

        if (position.y >= targetpos.y)
        {
            position.y = position.y + speed * -0.2f * Time.deltaTime;
        }
        else if (position.y < targetpos.y)
        {
            position.y = position.y + speed * 0.2f * Time.deltaTime;
        }
        transform.position = position;
    }

    bool isInErrorRange()
    {
        if (position.x > targetpos.x - errorRange && position.x < targetpos.x + errorRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void getNewTargetPos()
    {
        float x = Random.Range(-28, 28);
        float y = Random.Range(-10f, 12f);

        targetpos = new Vector3(x, y, 0);
    }

    public void getCurrentMoneyAndFoodInfo()
    {
       moneyPerSec= xmlReader.instance.getMoneyPerSec(seaAnimalName, level);
        foodAmount = xmlReader.instance.getFoodAmount(seaAnimalName, level);
        maxMoney = xmlReader.instance.getMaxMoney(seaAnimalName);
    }
}
