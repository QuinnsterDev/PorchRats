using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int droppedMoney;

    private Transform target;
    private bool inAttackRange;

    private float attackCooldownTime;

    public enum AIState
    {
        AttackTower,
        AttackDelivery
    }
    private AIState state;
    private void Start()
    {
        attackCooldownTime = attackCooldown;
        state = AIState.AttackDelivery;
    }

    private void Update()
    {
        if(target == null)
        {
            target = GameManager.instance.delivery.transform;
        }
        if (target != null && Vector3.Distance(gameObject.transform.position, target.position) <= attackRange)
        {
            inAttackRange = true;
        }

        switch (state)
        {
            case AIState.AttackDelivery:
                target = GameManager.instance.delivery.transform;
                AttackDelivery();
                if (inAttackRange)
                {
                    attackCooldownTime -= Time.deltaTime;
                    if (attackCooldownTime <= 0)
                    {
                        Attack();
                    }
                }
                break;

            case AIState.AttackTower:
                AttackTower();
                if (inAttackRange)
                {
                    attackCooldownTime -= Time.deltaTime;
                    if(attackCooldownTime <= 0)
                    {
                        Attack();
                    }
                }
                break;
        }
    }

    public virtual void AttackDelivery()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public virtual void AttackTower()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public virtual void EnemyDie()
    {
        OrderManager.instance.AddMoney(droppedMoney);
        WaveManager.instance.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            state = AIState.AttackTower;
            target = collision.transform;
        }
        if (collision.CompareTag("Delivery"))
        {
            state = AIState.AttackDelivery;
        }
    }

    void Attack()
    {

        if (Vector3.Distance(gameObject.transform.position, target.position) <= attackRange)
        {
            Health enemyHealth = target.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                AudioManager.instance.Play("Hit");
                attackCooldownTime = attackCooldown;
            }
            else
            {
                state = AIState.AttackDelivery;
                target = GameManager.instance.delivery.transform;

            }
        }
    }
}
