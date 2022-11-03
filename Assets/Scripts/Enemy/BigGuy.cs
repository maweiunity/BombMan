public class BigGuy : Enemy
{
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
}
