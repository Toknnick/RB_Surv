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
        if (!other.isTrigger && other.transform.CompareTag("Player") && other.transform.TryGetComponent(out Entity e))
        {
            isCanMove = false;
            StartCoroutine(DealDamage(e));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.transform.CompareTag("Player") && other.transform.TryGetComponent(out Entity e))
        {
            isCanMove = true;
            StopCoroutine(DealDamage(e));
        }
    }
}