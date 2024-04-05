using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public int bulletDamage;
    [SerializeField] private float bulletLifetime;
    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && collision.gameObject.transform.parent.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<Health>().TakeDamage(bulletDamage);
        }
    }
}
