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
    public float GuardRadius = 2f;
    public float GuardLastMoveX;

    [Header("State")]
    [SerializeField]
    public int moveDir = -1;

    [Header("Patrol")]
    [SerializeField]
    public Vector2 patrolLeft, patrolRight;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        // checkArea = GetComponentInChildren<Collider2D>();

        // 初始化巡逻范围
        initPatrolPoint();

        Debug.Log("ground:" + LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    // 初始化巡逻范围
    void initPatrolPoint()
    {
        patrolLeft = new Vector2(transform.position.x - GuardRadius, transform.position.y);
        patrolRight = new Vector2(transform.position.x + GuardRadius, transform.position.y);
    }

    void Patrol()
    {
        if (moveDir == 1 && transform.position.x > patrolRight.x)
        {
            moveDir = -1;
        }
        else if (moveDir == -1 && transform.position.x < patrolLeft.x)
        {
            moveDir = 1;
        }
        // 移动
        enemyRb.velocity = new Vector2(moveDir * MoveSpeed, transform.position.y);
        // 朝向
        transform.localScale = new Vector3(-moveDir, 1, 1);
        // 动画
        enemyAnim.SetFloat("Speed", Mathf.Abs(enemyRb.velocity.x));
        // 碰撞到墙了，
        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y - 0.2f);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.left * -moveDir, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayPos, Vector2.left * -moveDir, Color.green);
        if (hit)
        {
            moveDir = -moveDir;
        }
    }

}
