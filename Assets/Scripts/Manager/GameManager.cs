using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public static GameManager Instance;
    public PlayerController playerCtl;
    Door doorCtl;

    [Header("Game State")]
    public bool IsGameOver;

    // 敌人列表
    public List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        IsGameOver = playerCtl.IsDead;
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
        if (enemyList.Count < 1 && doorCtl)
        {
            doorCtl.OpenDoor();
        }
    }

    // 进行下一关
    public void EnterNextLevel()
    {
        SaveData(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 继续游戏
    public void ContiuneGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        playerCtl.Hp = PlayerPrefs.GetInt("PlayerHp");
    }

    // 保存数据
    public void SaveData(int level)
    {
        if (playerCtl)
        {
            PlayerPrefs.SetFloat("PlayerHp", playerCtl.Hp);
            PlayerPrefs.SetFloat("Level", level);
            PlayerPrefs.Save();
        }
    }

    // 获取存取数据
    public float GetData()
    {
        if (!PlayerPrefs.HasKey("PlayerHp"))
        {
            PlayerPrefs.SetFloat("PlayerHp", 30);
            PlayerPrefs.SetInt("Level", 1);
        }
        return PlayerPrefs.GetFloat("PlayerHp");
    }

    // 注册下一关的门
    public void RegisterDoor(Door obj)
    {
        doorCtl = obj;
    }

    // 注册玩家
    public void RegisterPlayer(PlayerController player)
    {
        playerCtl = player;
    }

}
