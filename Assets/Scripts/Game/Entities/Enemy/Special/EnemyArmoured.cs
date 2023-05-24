using System.Collections;
using UnityEngine;

public class EnemyArmoured : Enemy
{
    [SerializeField] protected float timeToPrepareToJump;
    [SerializeField] protected float timeToJump;
    [SerializeField] protected Material changingMaterial;
    [SerializeField] protected MeshRenderer meshRenderer;

    private Material defaultMateril;
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
        defaultMateril = meshRenderer.material;
        enemy.OnDie += () => { enemyAnimationController.SetDie(); ; };
    }

    protected override void Update()
    {
        if (!isJumping)
        {
            base.Update();

            if (isSetRunAnimation && navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= attackRange)
            {
                enemyAnimationController.SetAttack();
                StartCoroutine(Jump(player));
                isSetRunAnimation = false;
            }

        }

    }

    private IEnumerator Jump(EntityPlayer player)
    {
        isJumping = true;
        Vector3 position = player.transform.position;
        Vector3 startPosition = transform.position;
        meshRenderer.material = changingMaterial;
        yield return new WaitForSecondsRealtime(timeToPrepareToJump);
        float nowTime = 0;

        while (nowTime < timeToJump)
        {
            nowTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, position, nowTime / timeToJump);
            yield return null;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.AttackRange / 3f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                enemy.DealDamage(player, damagePerSec);
                Debug.Log("Дамажит " + gameObject.name);
            }
        }

        isSetRunAnimation = true;
        enemyAnimationController.SetRun();
        isJumping = false;
        meshRenderer.material = defaultMateril;
    }
}
