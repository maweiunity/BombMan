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
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - startTime) > CDTime)
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
                Vector3 dis = transform.position - item.transform.position;
                item.GetComponent<Rigidbody2D>().AddForce((-dis + Vector3.up) * explotionPower, ForceMode2D.Impulse);
            }
        }
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
