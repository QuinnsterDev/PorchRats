using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeBehavior : TowerBehavior
{
    [Header("Gnome Info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Animator animator;

    public override void TowerAttack()
    {
        AudioManager.instance.Play("GnomeShoot");
        animator.Play("GnomeShoot");
        base.TowerAttack();
        if(enemiesInRange[0] != null)
        {
            Vector3 enemyDirection = enemiesInRange[0].transform.position - transform.position;
            enemyDirection = enemyDirection.normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(enemyDirection * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<TowerBullet>().bulletDamage = damage;
        }



    }
}
