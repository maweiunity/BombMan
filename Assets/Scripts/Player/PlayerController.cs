using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 刚体对象
    Rigidbody2D playerRb;

    [Header("基础属性")]
    public float Hp = 100;
    public float MoveSpeed = 10;
    public float Jump = 20;

    [Header("状态")]
    public bool IsJump = false;
    public bool IsCanJump = true;
    public bool IsJumpBntDown = false;
    public bool IsGround = false;

    [Header("检测配置")]
    public Transform GroundCheckPoint;
    public float GroundCheckRadius = 0.2f;
    public LayerMask GroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        // GroundCheckPoint = transform.GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        checkIsGround();
        checkButtonDown();
    }

    private void FixedUpdate()
    {
        playerMove();
        playerJump();
    }

    // 移动
    void playerMove()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        // 移动
        playerRb.velocity = new Vector2(MoveSpeed * axis, playerRb.velocity.y);
        // 朝向
        if (axis != 0)
        {
            transform.localScale = new Vector3(axis, 1, 1);
        }
    }

    // 跳跃
    void playerJump()
    {
        if (IsJumpBntDown && IsCanJump)
        {
            IsCanJump = false;
            IsJump = true;
            playerRb.gravityScale = 3;
            playerRb.velocity = new Vector2(playerRb.velocity.x, Jump);
        }
        IsJumpBntDown = false;
    }

    // 是否按下跳跃键
    void checkButtonDown()
    {
        if (Input.GetButtonDown("Jump"))
        {
            IsJumpBntDown = true;
        }
    }

    // 是否在地面上检测
    void checkIsGround()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheckPoint.position, GroundCheckRadius, GroundLayer);
        if (IsGround)
        {
            IsCanJump = true;
            // 恢复重力
            playerRb.gravityScale = 1;
            // 状态重置
            IsJump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheckPoint.position, GroundCheckRadius);
    }
}
