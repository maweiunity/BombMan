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
    public int moveDir = -1;

    [Header("Patrol")]
    [SerializeField]
    protected Vector2 patrolLeft, patrolRight, moveTargetPos;
    protected float guardRadius = 2f;

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

    // 移动方向检测
    void direction()
    {
        if (moveDir == -1 && transform.position.x <= moveTargetPos.x)
        {
            moveDir = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            moveTargetPos = patrolRight;
        }
        else if (moveDir == 1 && transform.position.x >= moveTargetPos.x)
        {
            moveTargetPos = patrolLeft;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            moveDir = -1;
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
            Debug.Log("123123:" + hit.transform.position.x);
            moveDir = -moveDir;
            moveTargetPos = hit.transform.position;
            if (moveDir == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
