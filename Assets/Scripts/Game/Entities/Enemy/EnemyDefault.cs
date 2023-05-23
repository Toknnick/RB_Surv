using System;
using System.Collections;
using UnityEngine;

public class EnemyDefault : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemy.OnDie += () => { Destroy(gameObject); };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.transform.CompareTag("Player") && other.transform.TryGetComponent(out EntityPlayer player))
        {
            isCanMove = false;
            StartCoroutine(DealDamage(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.transform.CompareTag("Player") && other.transform.TryGetComponent(out EntityPlayer player))
        {
            isCanMove = true;
            StopCoroutine(DealDamage(player));
        }
    }
}