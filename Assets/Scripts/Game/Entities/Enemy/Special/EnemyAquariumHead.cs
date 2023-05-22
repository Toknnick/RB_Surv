using System.Collections;
using UnityEngine;

public class EnemyAquariumHead : Enemy
{
    [SerializeField] private GameObject head;
    [Header("Время на то, чтобы отрастить голову")]
    [SerializeField] private float timeToGrow;

    private bool isCanGrowHead = true;
    private bool isHaveHead = true;

    protected override void Start()
    {
        base.Start();
        head.SetActive(true);
        enemy.OnDie += GrowHead;
    }

    protected override void FixedUpdate()
    {
        if (isHaveHead)
            base.FixedUpdate();
    }

    private void GrowHead()
    {
        head.SetActive(false);
        isHaveHead = false;
        StartCoroutine(TryToGrow());
    }

    private IEnumerator TryToGrow()
    {
        enemy.CurrentHealth = enemy.MaxHealth;
        enemy.OnDie -= GrowHead;
        enemy.OnDie += () => { Destroy(gameObject); };
        yield return new WaitForSeconds(timeToGrow);
        Debug.Log("Отрастил голову");
        enemy.OnDie -= () => { Destroy(gameObject); };
        enemy.OnDie += GrowHead;
        enemy.CurrentHealth = enemy.MaxHealth;
        isHaveHead = true;
        head.SetActive(true);
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
