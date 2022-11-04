public class Cucumber : Enemy, IHurt
{

    // 吹灭炸弹
    void BlowOutBomb()
    {
        if (AttackTarget && AttackTarget.CompareTag("Bomb"))
        {
            AttackTarget.GetComponent<BombController>()?.TurnOff();
        }
    }
}
