using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    public int money;
    [SerializeField] private TMP_Text moneyText;

    public Dictionary<TowerInfo, int> ownedTowers = new Dictionary<TowerInfo, int>();
    public Dictionary<TowerInfo, int> orderedTowers = new Dictionary<TowerInfo, int>();
    public List<TowerInfo> towers;

    [SerializeField] private TowerInfo starterTower;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        UpdateUI();
        foreach (TowerInfo t in towers)
        {
            ownedTowers.Add(t, 0);
            orderedTowers.Add(t, 0);
        }
        ownedTowers[starterTower] = 1;
    }
    
    public void BuyTower(TowerInfo tower, int cost)
    {

        if((money - cost) >= 0)
        {
            AudioManager.instance.Play("OrderTower");
            TakeMoney(cost);
            orderedTowers[tower] += 1;
        }
    }

    public void AddMoney(int addedMoney)
    {
        money += addedMoney;
        UpdateUI();
    }

    public void TakeMoney(int takenMoney)
    {
        if ((money - takenMoney) >= 0)
        {
            money -= takenMoney;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = money.ToString();
    }

    public void OpenDeliveredTowers()
    {
        foreach (var tower in orderedTowers.Keys)
        {
            ownedTowers[tower] += orderedTowers[tower];
            
        }
        foreach (var tower in ownedTowers.Keys)
        {
            orderedTowers[tower] = 0;

        }

    }
}
