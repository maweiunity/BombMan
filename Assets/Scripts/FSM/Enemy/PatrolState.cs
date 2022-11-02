using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.AnimState = 0;
        enemy.SetMoveTarget();
    }

    public override void OnUpdate(Enemy enemy)
    {
        // 状态动画
        if (!enemy.EnemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.AnimState = 1;
            // 移动到目标点
            enemy.MoveToTarget();
        }

        // 是否已到巡逻点了
        if (Mathf.Abs(enemy.transform.position.x - enemy.MoveTargetPos.x) < 0.2f)
        {
            // 设置移动目标点
            enemy.ChangeState(enemy.PatrolState);
        }

        // 是否有敌人进入了范围
        if (enemy.AttackList.Count > 0)
        {
            enemy.ChangeState(enemy.AttackState);
        }
    }
}
