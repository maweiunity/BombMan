public abstract class EnemyBaseState
{
    // 进入对应状态
    public abstract void EnterState(Enemy enemy);

    // 进入状态后执行
    public abstract void OnUpdate(Enemy enemy);
}
