using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public bool IsDiaup = false;

    // 普通攻击
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                attackPlayer(other);
                break;
            case "Bomb":
                attackBomb(other);
                break;
        }
    }

    // 攻击玩家
    void attackPlayer(Collider2D other)
    {
        // 是否会击飞
        diaup(other);
        // 攻击到了，减伤
        other.GetComponent<IHurt>().HitHurt(gameObject.GetComponentInParent<Enemy>().AK);
    }

    // 攻击炸弹
    void attackBomb(Collider2D other)
    {
        diaup(other);
    }

    // 确定左右方向
    int getDiration(Collider2D other)
    {
        if (transform.position.x > other.transform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    // 击飞
    void diaup(Collider2D other)
    {
        if (IsDiaup)
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(getDiration(other), 1) * gameObject.GetComponentInParent<Enemy>().AK, ForceMode2D.Impulse);
        }
    }
}
