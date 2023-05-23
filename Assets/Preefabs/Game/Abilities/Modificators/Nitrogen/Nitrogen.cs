using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Nitrogen : MonoBehaviour
{
    private float radius;
    private float timeOfWork;

    public void TakeParametrs(float radius, float timeOfWork)
    {
        this.radius = radius;
        this.timeOfWork = timeOfWork;
        StartCoroutine(FreezeEnemies());
    }

    private IEnumerator FreezeEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
                if (collider.GetComponent<Enemy>() != null)
                    collider.GetComponent<Enemy>().enabled = false;
        }

        yield return new WaitForSecondsRealtime(timeOfWork);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.IsDestroyed() == false && collider.CompareTag("Enemy"))
                if (collider.GetComponent<Enemy>() != null)
                    collider.GetComponent<Enemy>().enabled = true;
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius * 2);
    }
}
