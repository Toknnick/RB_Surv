using System.Collections;
using UnityEngine;

public class EnemyArmoured : Enemy
{
    [SerializeField] protected float timeToPrepareToJump;
    [SerializeField] protected float timeToJump;
    [SerializeField] protected Material changingMaterial;
    [SerializeField] protected MeshRenderer meshRenderer;

    private Material defaultMateril;

    protected override void Start()
    {
        base.Start();
        defaultMateril = meshRenderer.material;
        enemy.OnDie += () => { Destroy(gameObject); };
    }

    protected override void FixedUpdate()
    {
        if (isCanMove)
            base.FixedUpdate();
    }

    private IEnumerator Jump(Entity player)
    {
        Vector3 position = player.transform.position;
        Vector3 startPosition = transform.position;
        meshRenderer.material = changingMaterial;
        isCanMove = false;
        float nowTime = 0;
        yield return new WaitForSecondsRealtime(timeToPrepareToJump);

        while (nowTime < timeToJump)
        {
            transform.position = Vector3.Lerp(startPosition, position, nowTime / timeToJump);
            nowTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(timeToJump);
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.AttackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                enemy.DealDamage(player, damagePerSec);
                Debug.Log("Дамажит " + gameObject.name);
            }
        }

        meshRenderer.material = defaultMateril;
        isCanMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.transform.CompareTag("Player") && other.transform.TryGetComponent(out Entity e))
        {
            isCanMove = false;
            StartCoroutine(Jump(e));
        }
    }
}
