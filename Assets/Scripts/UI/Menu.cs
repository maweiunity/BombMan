using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject MainPanel;
    public GameObject GameOverPanel;

    public Button MainMenuBtn;
    [Header("Pause Button Object")]
    public Button PauseBtn;
    public Button ReturnMainBtn;
    public Button ResumeBtn;
    public Button AgainBtn;
    [Header("GameOver Button Object")]
    public Button TryAgainBtn;
    public Button OverMainBtn;
    [Header("MainMenu Button Object")]
    public Button NewGameBtn;
    public Button ContiuneBtn;
    public Button QuitBtn;

    private void Start()
    {
        initBuildBtnEvents();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            GameOverPanel.SetActive(true);
        }
    }

    void initBuildBtnEvents()
    {
        // 暂停
        PauseBtn.onClick.AddListener(delegate
        {
            PauseGame();
        });
        // 返回主菜单
        ReturnMainBtn.onClick.AddListener(delegate
        {
            MainMenu();
        });
        // 继续游戏
        ResumeBtn.onClick.AddListener(delegate
        {
            ResumeGame();
        });
        // 再来一次
        AgainBtn.onClick.AddListener(delegate
        {
            AgainGame();
        });
        // 再来一次
        TryAgainBtn.onClick.AddListener(delegate
        {
            AgainGame();
        });
        // 返回主菜单
        OverMainBtn.onClick.AddListener(delegate
        {
            MainMenu();
        });

        // 重新游戏
        NewGameBtn.onClick.AddListener(delegate
        {
            RestartGame();
        });

        // 载入存档继续
        ContiuneBtn.onClick.AddListener(delegate
        {
            ContiuneGame();
        });

        // 退出游戏
        QuitBtn.onClick.AddListener(delegate
        {
            QuitGame();
        });
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

    // 游戏结束ui
    public void ShowGameOverUI(bool isActive)
    {
        GameOverPanel.SetActive(isActive);
    }

    // 主菜单
    public void StartGame(bool isActive)
    {
        MainPanel.SetActive(isActive);
    }


    // 重新开始新游戏
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    // 开始菜单
    public void MainMenu()
    {
        Debug.Log(12313);
        GameManager.Instance.SaveData(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

    // 继续游戏
    public void ContiuneGame()
    {
        GameManager.Instance.ContiuneGame();
    }

    // 重启当前关
    public void AgainGame()
    {
        PlayerPrefs.DeleteKey("PlayerHp");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
}
