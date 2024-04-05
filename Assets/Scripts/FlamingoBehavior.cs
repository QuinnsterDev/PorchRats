using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingoBehavior : TowerBehavior
{
    [Header("Flamingo Info")]
    [SerializeField] private Animator animator;
    public override void TowerAttack()
    {
        AudioManager.instance.Play("FlamingoAttack");
        animator.Play("FlamingoAttack");
        if(enemiesInRange[0] != null)
        {
            enemiesInRange[0].GetComponent<Health>().TakeDamage(damage);
        }
    }
}
