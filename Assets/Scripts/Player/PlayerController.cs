using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 刚体对象
    Rigidbody2D playerRb;
    Animator playerAnim;

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

    [Header("特效")]
    public GameObject JumpFXObj;
    public GameObject LandFXObj;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        // GroundCheckPoint = transform.GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        // 是否在地面检测
        checkIsGround();
        // 按键检测
        checkButtonDown();
        // 玩家动画
        playerAnimation();
    }

    private void FixedUpdate()
    {
        // 玩家移动
        playerMove();
        // 玩家跳跃
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
            // 跳跃特效
            JumpFXObj.SetActive(true);
            JumpFXObj.transform.position = transform.position + new Vector3(0, -0.54f, 0);
            // 是否能跳
            IsCanJump = false;
            // 跳跃状态
            IsJump = true;
            // 跳跃重力
            playerRb.gravityScale = 3;
            // 跳跃速度
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
            // 是否能跳跃了
            IsCanJump = true;
            // 恢复重力
            playerRb.gravityScale = 1;
            // 状态重置
            IsJump = false;
        }
    }

    // 玩家动画
    void playerAnimation()
    {
        // 跳跃,下落
        playerAnim.SetFloat("JumpY", playerRb.velocity.y);
        // 跑步,站立
        playerAnim.SetFloat("Speed", Mathf.Abs(playerRb.velocity.x));
        // 站立
        playerAnim.SetBool("IsGround", IsGround);
    }

    // 下落特效处理
    void landFX()
    {
        LandFXObj.SetActive(true);
        LandFXObj.transform.position = transform.position + new Vector3(0, -0.8f, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheckPoint.position, GroundCheckRadius);
    }
}
