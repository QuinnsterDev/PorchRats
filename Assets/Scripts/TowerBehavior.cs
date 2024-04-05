using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public int damage;
    [SerializeField] private float attackCooldown;
    private float attackCooldownTime;

    public List<Enemy> enemiesInRange;
    private void Start()
    {
        attackCooldownTime = attackCooldown;
    }
    private void Update()
    {
        if(enemiesInRange.Count > 0)
        {
            attackCooldownTime -= Time.deltaTime;
        }

        if(attackCooldownTime <= 0)
        {
            TowerAttack();
            attackCooldownTime = attackCooldown;
        }
    }
    public virtual void TowerAttack()
    {

    }
    public void TowerDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.GetComponent<Enemy>());
        }
    }
}
