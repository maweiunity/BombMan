using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Init Component")]
    public Rigidbody2D EnemyRb;
    public Animator EnemyAnim;
    // protected Collider2D checkArea;

    [Header("Common Attribute")]
    public float Hp = 100;
    public float AK = 10;
    public float MoveSpeed = 2;
    public float AT = 1f;
    public float AttackRate = 1f;
    public float AttackRadius = 1f;
    public float SkillAttackPower = 10;
    public float SkillAttackRadius = 1.2f;
    protected float nextAttackTime;
    public bool IsBoss;

    [Header("State")]
    public int moveDir = 1;
    public int AnimState = 0;
    public bool IsChange = false;
    public bool IsDead = false;
    public bool HasBomb = false;

    [Header("Animation Mode")]
    public EnemyBaseState CurrentState;
    public EnemyBaseState PatrolState = new PatrolState();
    public EnemyBaseState AttackState = new AttackState();

    [Header("Patrol")]
    public Vector2 PatrolLeft, PatrolRight, MoveTargetPos;
    protected float guardRadius = 5f;

    [Header("Attack")]
    public Transform AttackTarget;
    public List<Transform> AttackList = new List<Transform>();

    private void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        // checkArea = GetComponentInChildren<Collider2D>();

        // 初始化巡逻范围
        initPatrolPoint();
        // 初始化运行模式
        ChangeState(PatrolState);
        // 初始化boss血量
        if (IsBoss) UIManager.Instance.InitBossHealthBar(Hp);
    }

    private void Update()
    {
        if (IsDead) return;

        CurrentState.OnUpdate(this);
        EnemyAnim.SetInteger("State", AnimState);

        // 显示血量
        if (IsBoss) UIManager.Instance.UpdateBossHp(Hp);
    }

    // 初始化巡逻范围
    void initPatrolPoint()
    {
        // 初始化左右移动目标点
        PatrolLeft = new Vector2(transform.position.x - guardRadius, transform.position.y);
        PatrolRight = new Vector2(transform.position.x + guardRadius, transform.position.y);
        // 初始化移动目标点
        MoveTargetPos = PatrolRight;
    }

    // 状态切换
    public void ChangeState(EnemyBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    // 移动到目标
    public void MoveToTarget()
    {
        if (HasBomb) return;
        // 障碍检测
        moveToBarrier();
        // 移动
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(MoveTargetPos.x, transform.position.y), MoveSpeed * Time.deltaTime);
        // 朝向
        faceDirection();
    }

    // 普通攻击
    public void CommonAttack()
    {
        if (Vector2.Distance(transform.position, MoveTargetPos) < AttackRadius && Time.time >= nextAttackTime)
        {
            // 设置下次攻击时间
            nextAttackTime = Time.time + AttackRate;
            // 设置动画
            EnemyAnim.SetTrigger("Attack");
        }
    }

    // 技能攻击
    public virtual void SkillAttack()
    {
        if (Vector2.Distance(transform.position, MoveTargetPos) < SkillAttackRadius && Time.time >= nextAttackTime)
        {
            // 设置下次攻击时间
            nextAttackTime = Time.time + AttackRate;
            // 设置动画
            EnemyAnim.SetTrigger("Skill");
        }
    }

    // 移动障碍检测
    void moveToBarrier()
    {
        // 碰撞到墙了，
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y - 0.2f);
        // 发射检测射线
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.right * moveDir, 0.4f, LayerMask.GetMask("Ground"));
        // 画射线，用于调试
        Debug.DrawRay(rayPos, Vector2.right * moveDir, Color.green);
        // 如果碰到墙，换方向
        if (hit)
        {
            MoveTargetPos = new Vector2(transform.position.x + (moveDir * 0.1f), transform.position.y);
        }
    }

    // 人物面朝方向
    void faceDirection()
    {
        if (transform.position.x <= MoveTargetPos.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            moveDir = 1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            moveDir = -1;
        }
    }

    // 设置移动目标点
    public void SetMoveTarget()
    {
        // 切换目标点
        if (Mathf.Abs(transform.position.x - PatrolLeft.x) - Mathf.Abs(transform.position.x - PatrolRight.x) > 0)
        {
            MoveTargetPos = PatrolLeft;
        }
        else
        {
            MoveTargetPos = PatrolRight;
        }
    }

    // 进行范围
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!AttackList.Contains(other.transform) && !HasBomb)
        {
            AttackList.Add(other.transform);
        }
    }

    // 退出范围
    private void OnTriggerExit2D(Collider2D other)
    {
        if (AttackList.Contains(other.transform))
        {
            AttackList.Remove(other.transform);
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.FindChild("Alert").gameObject.SetActive(true);
    }

}
