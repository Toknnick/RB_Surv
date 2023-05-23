using UnityEngine;

public class TeleporterOfEntities : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityPlayer player))
            player.TakeDamage(null, 99999999);
        else
            collision.gameObject.GetComponent<EntityEnemy>().TakeDamage(99999999);
    }
}
