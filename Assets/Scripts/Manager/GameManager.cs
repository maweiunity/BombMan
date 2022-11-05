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

    string playerHpKey = "PlayerHp";

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
        if (IsGameOver)
        {
            UIManager.Instance.ShowGameOverUI(true);
        }
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

    // 重启当前关
    public void AgainGame()
    {
        PlayerPrefs.DeleteKey(playerHpKey);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        SceneManager.LoadScene(PlayerPrefs.GetInt(playerHpKey));
    }

    // 退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    // 重新开始新游戏
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    // 开始菜单
    public void StartMenu()
    {
        SaveData(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

    // 保存数据
    public void SaveData(int level)
    {
        PlayerPrefs.SetFloat(playerHpKey, playerCtl.Hp);
        PlayerPrefs.SetFloat("Level", level);
        PlayerPrefs.Save();
    }

    // 获取存取数据
    public float GetData()
    {
        if (!PlayerPrefs.HasKey(playerHpKey))
        {
            PlayerPrefs.SetFloat(playerHpKey, 30);
            PlayerPrefs.SetInt("Level", 1);
        }
        return PlayerPrefs.GetFloat(playerHpKey);
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
