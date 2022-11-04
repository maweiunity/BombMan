using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject HpBar;

    public GameObject PauseMenu;
    public Slider BossMaxHP;

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
                HpBar.transform.GetChild(0).gameObject.SetActive(true);
                HpBar.transform.GetChild(1).gameObject.SetActive(true);
                HpBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                HpBar.transform.GetChild(0).gameObject.SetActive(true);
                HpBar.transform.GetChild(1).gameObject.SetActive(true);
                HpBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                HpBar.transform.GetChild(0).gameObject.SetActive(true);
                HpBar.transform.GetChild(1).gameObject.SetActive(false);
                HpBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            default:
                HpBar.transform.GetChild(0).gameObject.SetActive(false);
                HpBar.transform.GetChild(1).gameObject.SetActive(false);
                HpBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void InitBossHealthBar(float health)
    {
        BossMaxHP.maxValue = health;
    }

    public void UpdateBossHp(float health)
    {
        BossMaxHP.value = health;
    }
}
