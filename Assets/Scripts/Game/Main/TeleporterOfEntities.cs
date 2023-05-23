using UnityEngine;

public class TeleporterOfEntities : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Entity>().TakeDamage(99999999);
    }
}
