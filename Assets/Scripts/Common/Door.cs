using UnityEngine;

public class Door : MonoBehaviour
{
    Collider2D doorColl;
    Animator doorAnim;


    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        doorColl = GetComponent<Collider2D>();

        // 注册
        GameManager.Instance.RegisterDoor(this);
    }

    // 打开下一关的门
    public void OpenDoor()
    {
        doorAnim.Play("Open");
        doorColl.enabled = true;
    }

    // 进入门里,进入下一关
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.EnterNextLevel();
        }
    }
}
