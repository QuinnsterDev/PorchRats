using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers")]
public class TowerInfo : ScriptableObject
{
    public new string name;
    public string description;
    public GameObject towerPrefab;
    public Sprite towerSprite;
}
