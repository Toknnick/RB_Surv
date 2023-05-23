using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float damage;
    private float radius;
    private float timeOfWork;
    private Rigidbody rb;
    private float speedGrenade;

    public void TakeParametrs(float damage, float radius, float timeOfWork, float speedGrenade)
    {
        rb = GetComponent<Rigidbody>();
        this.damage = damage;
        this.radius = radius;
        this.timeOfWork = timeOfWork;
        this.speedGrenade = speedGrenade;
        StartCoroutine(Use());
    }

    private IEnumerator Use()
    {
        rb.velocity = GameManager.instance.PlayerController.GetDirection() * speedGrenade;
        yield return new WaitForSecondsRealtime(timeOfWork);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
                collider.GetComponent<Entity>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}

