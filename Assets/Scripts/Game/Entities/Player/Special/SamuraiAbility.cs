using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SamuraiAbility : SpecialAbility
{
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float damage = 10f;

    private bool isExist = false;

    public override void UseSpecial()
    {
        if (!isExist)
            Instantiate(this);
        else
        {
            Entity player = GameManager.instance.Player;
            Vector3 direction = player.gameObject.transform.forward;
            StartCoroutine(Use(direction, player));
        }
    }

    private void Start()
    {
        isExist = true;
        Entity player = GameManager.instance.Player;
        Vector3 direction = player.gameObject.transform.forward;
        StartCoroutine(Use(direction, player));
    }

    private IEnumerator Use(Vector3 direction, Entity player)
    {
        float currentDistance = 0f;
        float dashStep = 5f; // –ассто€ние перемещени€ за каждый кадр

        while (currentDistance < dashDistance)
        {
            player.transform.position += direction * dashStep;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, player.AttackRange);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EntityEnemy enemy = collider.GetComponent<EntityEnemy>();
                    enemy.TakeDamage(damage);
                }
            }

            currentDistance += dashStep;
            yield return null;
        }
    }
}
