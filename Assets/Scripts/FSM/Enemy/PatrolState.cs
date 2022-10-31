using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.AnimState = 0;
        enemy.ChangeState(enemy.PatrolState);
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (Mathf.Abs(enemy.transform.position.x - enemy.MoveTargetPos.x) < 0.1f)
        {
            enemy.AnimState = 0;
            // 设置移动目标点
            enemy.SetMoveTarget();
        }
        // 状态动画
        if (!enemy.EnemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.AnimState = 1;
            // 移动到目标点
            enemy.MoveToTarget();
        }
    }
}
