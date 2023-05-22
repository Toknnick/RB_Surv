using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected RectTransform hpBar;
    [SerializeField] protected RectTransform hp;
    [SerializeField] protected SphereCollider attackZone;
    [SerializeField] protected Canvas canvas;

    protected float speed;
    protected bool isCanMove = true;
    protected float damagePerSec;
    protected Entity enemy;
    protected GameObject player;
    protected Vector3 moveDirection;

    protected virtual void Start()
    {
        player = GameManager.instance.PlayerTransform;
        enemy = GetComponent<Entity>();
        speed = enemy.Speed;
        damagePerSec = enemy.DamagePerSec;
        attackZone.radius = enemy.AttackRange;
        canvas.worldCamera = GameManager.instance.PlayerController.CameraFollower.GetComponent<Camera>();
        enemy.OnHealthChange += () => { hp.gameObject.SetActive(enemy.CurrentHealth != enemy.MaxHealth); var a = hpBar.localScale; a.x = enemy.CurrentHealth / enemy.MaxHealth; hpBar.localScale = a; };
    }

    protected virtual void FixedUpdate()
    {
        if (isCanMove)
        {
            moveDirection = (player.transform.position - transform.position);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
            transform.rotation = rotation;
            transform.Translate(speed * Time.deltaTime * moveDirection.normalized, Space.World);
        }
    }

    protected virtual IEnumerator DealDamage(Entity player)
    {
        while (true)
        {
            enemy.DealDamage(player, damagePerSec);
            Debug.Log("Дамажит " + gameObject.name);
            yield return new WaitForSecondsRealtime(enemy.AttackRate);
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(gameObject.transform.position, enemy.AttackRange * 2f);
    }
}
