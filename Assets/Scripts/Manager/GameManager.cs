using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public static GameManager Instance;
    public PlayerController PlayerCtl;

    [Header("Game State")]
    public bool IsGameOver;

    string playerHpKey = "PlayerHp";

    // 敌人列表
    public List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerCtl = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        IsGameOver = PlayerCtl.IsDead;
        if (IsGameOver) UIManager.Instance.ShowGameOverUI(true);
    }

    // 添加敌人到列表
    public void AddEnemy(Enemy obj)
    {
        enemyList.Add(obj);
    }

    // 删除敌人
    public void RemoveEnemy(Enemy obj)
    {
        enemyList.Remove(obj);
        if (enemyList.Count < 1)
        {
            FindObjectOfType<Door>().OpenDoor();
        }
    }

    // 重启当前关
    public void AgainGame()
    {
        PlayerPrefs.DeleteKey(playerHpKey);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 进行下一关
    public void EnterNextLevel()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    // 保存数据
    public void SaveData()
    {
        PlayerPrefs.SetFloat(playerHpKey, PlayerCtl.Hp);
        PlayerPrefs.Save();
    }

    // 获取存取数据
    public float GetData()
    {
        if (!PlayerPrefs.HasKey(playerHpKey))
        {
            PlayerPrefs.SetFloat(playerHpKey, PlayerCtl.Hp);
        }
        return PlayerPrefs.GetFloat(playerHpKey);
    }
}
