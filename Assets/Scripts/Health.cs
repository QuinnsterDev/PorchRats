using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthBarSlider;
    private Canvas healthBarCanvas;
    [SerializeField] private Vector3 healthBarOffset;

    private Enemy enemyBehavior;
    private TowerBehavior towerBehavior;

    private void Awake()
    {


    }
    private void Start()
    {
        healthBarCanvas = GameManager.instance.healthBarCanvas;

        GameObject healthBar = Instantiate(healthBarPrefab, healthBarCanvas.transform);
        healthBarSlider = healthBar.GetComponent<Slider>();

        enemyBehavior = gameObject.GetComponent<Enemy>();
        towerBehavior = gameObject.GetComponent<TowerBehavior>();

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }

        Vector3 healthBarPos = gameObject.transform.position + healthBarOffset;
        if(healthBarSlider != null)
        {
            healthBarSlider.gameObject.transform.position = healthBarPos;
        }

    }
    void UpdateUI()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateUI();

        if(enemyBehavior != null)
        {
            AudioManager.instance.Play("EnemyHurt");
        }

        if(health <= 0)
        {
            if(enemyBehavior != null)
            {
                enemyBehavior.EnemyDie();
            }
            if (towerBehavior != null)
            {
                towerBehavior.TowerDestroyed();
            }
            if(gameObject.tag == "Delivery")
            {
                GameManager.instance.LoadScene("DeathScreen");
            }
            Destroy(healthBarSlider.gameObject);
        }
    }
}
