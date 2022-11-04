using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Bar")]
    public GameObject HpPanel;
    public Slider BossMaxHP;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    // 显示血条
    public void ShowHealth(float hp)
    {
        switch (hp / 10)
        {
            case 3:
                HpPanel.transform.GetChild(0).gameObject.SetActive(true);
                HpPanel.transform.GetChild(1).gameObject.SetActive(true);
                HpPanel.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                HpPanel.transform.GetChild(0).gameObject.SetActive(true);
                HpPanel.transform.GetChild(1).gameObject.SetActive(true);
                HpPanel.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                HpPanel.transform.GetChild(0).gameObject.SetActive(true);
                HpPanel.transform.GetChild(1).gameObject.SetActive(false);
                HpPanel.transform.GetChild(2).gameObject.SetActive(false);
                break;
            default:
                HpPanel.transform.GetChild(0).gameObject.SetActive(false);
                HpPanel.transform.GetChild(1).gameObject.SetActive(false);
                HpPanel.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    // 暂停游戏
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    // 继续游戏
    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // 初始化boss血条
    public void InitBossHealthBar(float health)
    {
        BossMaxHP.maxValue = health;
    }

    // 更新boss血条
    public void UpdateBossHp(float health)
    {
        BossMaxHP.value = health;
    }

    // 游戏结束ui
    public void ShowGameOverUI(bool isActive)
    {
        GameOverPanel.SetActive(isActive);
    }
}
