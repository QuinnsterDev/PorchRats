using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemyInfo : ScriptableObject
{
    public new string enemyName;
    public GameObject enemyPrefab;
    public int startingWave;
    public int addedPerWave;
}
