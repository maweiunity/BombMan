public class Cucumber : Enemy
{
    // 吹灭炸弹
    void BlowOutBomb()
    {
        if (AttackTarget.CompareTag("Bomb"))
        {
            AttackTarget.GetComponent<BombController>()?.TurnOff();
        }
    }
}
