using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.AnimState = 2;
        enemy.MoveTargetPos = enemy.AttackList[0].position;
        enemy.AttackTarget = enemy.AttackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        // 如果范围内没有敌人了,切换为巡逻模式
        if (enemy.AttackList.Count < 1)
        {
            enemy.ChangeState(enemy.PatrolState);
            enemy.AttackTarget = null;
            return;
        }

        // 攻击
        attackTarget(enemy);

        // 更新目录坐标
        enemy.MoveTargetPos = enemy.AttackTarget.position;

        // 攻击目标
        switch (enemy.AttackTarget.tag)
        {
            case "Player":
                enemy.CommonAttack();
                break;
            case "Bomb":
                enemy.SkillAttack();
                break;
        }

        // 移动动目标点
        enemy.MoveToTarget();
    }

    // 攻击
    void attackTarget(Enemy enemy)
    {
        if (enemy.AttackList.Count == 1)
        {
            enemy.AttackTarget = enemy.AttackList[0];
        }
        else if (enemy.AttackList.Count > 1)
        {
            for (int i = 0; i < enemy.AttackList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.AttackList[i].position.x) < Mathf.Abs(enemy.transform.position.x - enemy.MoveTargetPos.x))
                {
                    enemy.AttackTarget = enemy.AttackList[i];
                }
            }
        }
    }
}
