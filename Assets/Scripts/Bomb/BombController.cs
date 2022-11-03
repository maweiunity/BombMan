using UnityEngine;

public class BombController : MonoBehaviour
{
    Rigidbody2D bombRb;
    Animator bombAnim;
    Collider2D bombCollid;

    [Header("Base Attr")]
    public float explotionPower;
    public float explotionRadius;
    public float CDTime;
    float startTime;
    public bool State;
    public float Attack;

    [Header("Check")]
    public LayerMask explotionLM;


    // Start is called before the first frame update
    void Start()
    {
        // 初始化组件
        bombRb = GetComponent<Rigidbody2D>();
        bombAnim = GetComponent<Animator>();
        bombCollid = GetComponent<Collider2D>();
        // 开始计时
        startTime = Time.time;
        State = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (State && (Time.time - startTime) > CDTime)
        {
            bombAnim.Play("Explotion");
        }
    }

    // 炸弹爆炸
    void bombExplotion()
    {
        // 取消碰撞体的激活状态
        bombCollid.enabled = false;
        Collider2D[] collObj = Physics2D.OverlapCircleAll(transform.position, explotionRadius, explotionLM);
        // 因为取消了碰撞体，就会下落，所以修改重力为0
        bombRb.gravityScale = 0;
        if (collObj.Length > 0)
        {
            foreach (var item in collObj)
            {
                // 添加爆炸力
                Vector3 dis = transform.position - item.transform.position;
                item.GetComponent<Rigidbody2D>().AddForce((-dis + Vector3.up) * explotionPower, ForceMode2D.Impulse);

                // 如果是关闭的炸弹，重新激活
                if (item.CompareTag("Bomb") && item.GetComponent<BombController>().State == false)
                {
                    item.GetComponent<BombController>().TurnOn();
                }
                else if (item.CompareTag("Player") || item.CompareTag("Enemy"))
                {
                    // 爆炸攻击
                    item.GetComponent<IHurt>()?.HitHurt(Attack);
                }
            }
        }
    }

    // 关闭炸弹
    public void TurnOff()
    {
        // 更改状态
        State = false;
        // 播放熄灭动画
        bombAnim.Play("Off");
        // 修改layer层
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public void TurnOn()
    {
        // 开启状态
        State = true;
        // 播放动画
        bombAnim.Play("On");
        // 修改layer层
        gameObject.layer = LayerMask.NameToLayer("Bomb");
        // 开始计时
        startTime = Time.time;
    }

    // 销毁
    void distory()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
