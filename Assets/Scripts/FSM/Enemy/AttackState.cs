using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("p wh ;");
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
        attack(enemy);
    }

    // 攻击
    void attack(Enemy enemy)
    {

        for (int i = 0; i < enemy.AttackList.Count; i++)
        {
            if (Mathf.Abs(enemy.transform.position.x - enemy.AttackList[i].position.x) < Mathf.Abs(enemy.transform.position.x - enemy.MoveTargetPos.x))
            {
                enemy.AttackTarget = enemy.AttackList[i];
            }
        }

        // 更新目录坐标
        enemy.AnimState = 1;
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
}
