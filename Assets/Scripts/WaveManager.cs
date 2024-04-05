using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public List<Wave> waves;
    [SerializeField] private TMP_Text waveText;
    public int waveNumber = 0;

    [SerializeField] private Transform[] spawnLocations;
    public List<GameObject> spawnedEnemies;

    private bool isEndless = false;
    private int incrementalMultiplier = 2;
    private int incrementalAddition = 1;
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

    void Start()
    {

    }

    public void StartWave()
    {
        waveText.gameObject.SetActive(true);
        waveText.text = "ROUND " + (waveNumber + 1).ToString();
        if(waveNumber > waves.Count-1)
        {
            isEndless = true;
            StartCoroutine(SpawnWaveEnemies(waves[waves.Count-1]));

        }
        else
        {
            StartCoroutine(SpawnWaveEnemies(waves[waveNumber]));
        }
    }

    public void EndWave()
    {
        waveNumber += 1;
        GameManager.instance.StartOrderPhase();
        waveText.text = "ORDER PHASE";
        if(isEndless == true)
        {
            incrementalMultiplier += 1;
        }

    }

    IEnumerator SpawnWaveEnemies(Wave spawningWave)
    {
        
        bool enemiesAlive = true;
        foreach (EnemyInfo enemy in spawningWave.waveEnemies)
        {
            int location = Random.Range(0, spawnLocations.Length);
            GameObject spawnedEnemy = Instantiate(enemy.enemyPrefab, spawnLocations[location].position, Quaternion.identity);

            spawnedEnemies.Add(spawnedEnemy);
            yield return new WaitForSeconds(spawningWave.spawnDelay);
        }

        if (isEndless)
        {
            foreach (EnemyInfo enemy in spawningWave.waveEnemies)
            {
                for (int i = 0; i < enemy.addedPerWave * incrementalMultiplier; i++)
                {
                    int location = Random.Range(0, spawnLocations.Length);
                    GameObject spawnedEnemy = Instantiate(enemy.enemyPrefab, spawnLocations[location].position, Quaternion.identity);

                    spawnedEnemies.Add(spawnedEnemy);
                    yield return new WaitForSeconds(spawningWave.spawnDelay);
                }

            }

        }
        while (enemiesAlive == true)
        {
            yield return new WaitForSeconds(spawningWave.spawnDelay);
            if(spawnedEnemies.Count == 0) { enemiesAlive = false; }
        }

        EndWave();
    }
}

[System.Serializable]
public class Wave
{
    public float spawnDelay;
    public EnemyInfo[] waveEnemies;
}
