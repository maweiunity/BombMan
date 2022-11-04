public class Whale : Enemy, IHurt
{
    float scale = 1.1f;

    // 受伤
    public void HitHurt(float hurtVal)
    {
        // 播放动画
        EnemyAnim.SetTrigger("Hurt");
        // 减生命值
        Hp -= hurtVal;
        if (Hp < 1)
        {
            Hp = 0;
            EnemyAnim.SetBool("Dead", true);
            IsDead = true;
        }
    }

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
