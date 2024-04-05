using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderButton : MonoBehaviour
{
    public int cost;
    public TowerInfo tower;

    private void Update()
    {
    }
    public void OrderTower()
    {
        OrderManager.instance.BuyTower(tower, cost);
    }
}
