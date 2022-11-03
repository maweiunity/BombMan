using UnityEngine;

public class AttackHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHurt>().HitHurt(gameObject.GetComponentInParent<Enemy>().AK);
        }
    }
}
