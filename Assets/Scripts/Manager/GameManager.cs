using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController PlayerCtl;

    public bool IsGameOver;

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

    public void AgainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
