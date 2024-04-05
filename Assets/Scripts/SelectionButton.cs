using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectionButton : MonoBehaviour
{
    [SerializeField] private TowerInfo tower;
    [SerializeField] private TMP_Text ownedText;
    private void Update()
    {
        if (OrderManager.instance.ownedTowers.ContainsKey(tower))
        {
            ownedText.text = OrderManager.instance.ownedTowers[tower].ToString();
        }
    }
    public void UpdateOwnedText()
    {

    }
}
