using UnityEngine;

public class AttackHit : MonoBehaviour
{
    // 普通攻击
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 攻击到了，减伤
            other.GetComponent<IHurt>().HitHurt(gameObject.GetComponentInParent<Enemy>().AK);
        }
    }
}
