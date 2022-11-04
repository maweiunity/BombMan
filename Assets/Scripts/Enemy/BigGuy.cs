using UnityEngine;

public class BigGuy : Enemy, IHurt
{
    Transform pickUpPoint;
    // public float SkillAttack = 10;

    [System.Obsolete]
    private void Awake()
    {
        pickUpPoint = transform.FindChild("PickUpBomb").transform;
    }

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

    // 捡起炸弹
    public void PickUpBomb()
    {
        if (!HasBomb && AttackTarget.CompareTag("Bomb"))
        {
            // 修改炸弹位置
            AttackTarget.gameObject.transform.position = pickUpPoint.position;
            // 修改刚体为运动学
            AttackTarget.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            AttackTarget.SetParent(pickUpPoint);
            AttackList.Clear();
            // 修改身上为炸弹状态
            HasBomb = true;
        }
    }

    // 扔掉炸弹
    public void ThrowAwayBomb()
    {
        if (HasBomb)
        {
            AttackTarget.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            AttackTarget.SetParent(pickUpPoint.parent.parent);
            // 扔炸弹
            int dir = -1;
            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                dir = -1;
            }
            else
            {
                dir = 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            AttackTarget.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * SkillAttackPower, ForceMode2D.Impulse);
            HasBomb = false;
            AttackTarget = null;
        }
    }
}
