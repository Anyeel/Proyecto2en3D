using UnityEngine;

public class CollectibleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameEvents.CollectibleEarned.Invoke();
        }
    }
}
