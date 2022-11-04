public class Whale : Enemy, IHurt
{
    float scale = 1.1f;

    public void EatBomb()
    {
        if (AttackTarget && AttackTarget.CompareTag("Bomb"))
        {
            AttackTarget.gameObject?.SetActive(false);
            AttackTarget.transform.SetParent(transform);
            transform.localScale *= scale;
        }
    }
}
