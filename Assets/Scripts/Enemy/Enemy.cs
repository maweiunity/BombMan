using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Init Component")]
    protected Rigidbody2D enemyRb;
    protected Animator enemyAnim;
    // protected Collider2D checkArea;

    [Header("Common Attribute")]
    public float HP = 100;
    public float AK = 10;
    public float MoveSpeed = 500;
    public float AT = 1f;

    [Header("State")]
    [SerializeField]
    public int moveDir = 1;

    [Header("Patrol")]
    [SerializeField]
    protected Vector2 patrolLeft, patrolRight, moveTargetPos;
    protected float guardRadius = 5f;

    [Header("Attack")]
    [SerializeField]
    protected List<Transform> attackList = new List<Transform>();

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        // checkArea = GetComponentInChildren<Collider2D>();

        // 初始化巡逻范围
        initPatrolPoint();
    }

    private void Update()
    {
        direction();
        moveToTarget();

        setAnimation();
    }

    // 初始化巡逻范围
    void initPatrolPoint()
    {
        patrolLeft = new Vector2(transform.position.x - guardRadius, transform.position.y);
        patrolRight = new Vector2(transform.position.x + guardRadius, transform.position.y);
        moveTargetPos = patrolRight;
    }

    void Patrol()
    {
        // 移动
        enemyRb.velocity = new Vector2(moveDir * MoveSpeed, transform.position.y);
        // 朝向
        transform.localScale = new Vector3(-moveDir, 1, 1);
        // 动画
        enemyAnim.SetFloat("Speed", Mathf.Abs(enemyRb.velocity.x));
    }

    // 移动到目标
    void moveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveTargetPos.x, transform.position.y), MoveSpeed * Time.deltaTime);
    }

    // 动画
    void setAnimation()
    {
        enemyAnim.SetFloat("Speed", enemyRb.velocity.x);
    }

    // 移动方向检测
    void direction()
    {
        float distance = Vector2.Distance(transform.position, moveTargetPos);
        if (Mathf.Abs(distance) <= 0.2f)
        {
            if (moveDir == 1)
            {
                moveDir = -1;
                moveTargetPos = patrolLeft;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                moveDir = 1;
                moveTargetPos = patrolRight;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        // 碰撞到墙了，
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y - 0.2f);
        // 发射检测射线
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.right * moveDir, 0.5f, LayerMask.GetMask("Ground"));
        // 画射线，用于调试
        Debug.DrawRay(rayPos, Vector2.right * moveDir, Color.green);
        // 如果碰到墙，换方向
        if (hit)
        {
            moveTargetPos = transform.position;
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
}
