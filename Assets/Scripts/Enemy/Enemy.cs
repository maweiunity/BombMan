using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Init Component")]
    public Rigidbody2D EnemyRb;
    public Animator EnemyAnim;
    // protected Collider2D checkArea;

    [Header("Common Attribute")]
    public float HP = 100;
    public float AK = 10;
    public float MoveSpeed = 500;
    public float AT = 1f;

    [Header("State")]
    public int MoveDir = 1;
    public int AnimState = 0;
    public EnemyBaseState CurrentState;
    public EnemyBaseState PatrolState = new PatrolState();
    public EnemyBaseState AttackState = new AttackState();

    [Header("Patrol")]
    public Vector2 PatrolLeft, PatrolRight, MoveTargetPos;
    protected float guardRadius = 5f;

    [Header("Attack")]
    [SerializeField]
    protected List<Transform> attackList = new List<Transform>();

    private void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        // checkArea = GetComponentInChildren<Collider2D>();

        // 初始化巡逻范围
        initPatrolPoint();
        CurrentState = PatrolState;
    }

    private void Update()
    {
        EnemyAnim.SetInteger("State", AnimState);
        CurrentState.OnUpdate(this);
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
        // 障碍检测
        moveToBarrier();
        // 移动
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(MoveTargetPos.x, transform.position.y), MoveSpeed * Time.deltaTime);
        // 朝向
        faceDirection();
    }

    // 移动障碍检测
    void moveToBarrier()
    {
        // 碰撞到墙了，
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y - 0.2f);
        // 发射检测射线
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.right * MoveDir, 0.5f, LayerMask.GetMask("Ground"));
        // 画射线，用于调试
        Debug.DrawRay(rayPos, Vector2.right * MoveDir, Color.green);
        // 如果碰到墙，换方向
        if (hit)
        {
            MoveTargetPos = transform.position;
        }
    }

    // 人物面朝方向
    void faceDirection()
    {
        if (MoveDir == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // 设置移动目标点
    public void SetMoveTarget()
    {
        // 切换目标点
        if (transform.position.x >= MoveTargetPos.x)
        {
            MoveDir = -1;
            MoveTargetPos = PatrolLeft;
        }
        else
        {
            MoveDir = 1;
            MoveTargetPos = PatrolRight;
        }
    }

    // 进行范围
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!attackList.Contains(other.transform))
        {
            attackList.Add(other.transform);
        }
    }

    // 退出范围
    private void OnTriggerExit2D(Collider2D other)
    {
        if (attackList.Contains(other.transform))
        {
            attackList.Remove(other.transform);
        }
    }

    // void Patrol()
    // {
    //     // 移动
    //     enemyRb.velocity = new Vector2(MoveDir * MoveSpeed, transform.position.y);
    //     // 朝向
    //     transform.localScale = new Vector3(-MoveDir, 1, 1);
    //     // 动画
    //     enemyAnim.SetFloat("Speed", Mathf.Abs(enemyRb.velocity.x));
    // }


    // 动画
    // void setAnimation()
    // {
    //     enemyAnim.SetFloat("Speed", enemyRb.velocity.x);
    // }
}
