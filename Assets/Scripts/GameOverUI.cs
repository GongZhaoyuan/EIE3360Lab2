using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public float restartDelay = 2f;

    private void Start()
    {
        // 隐藏游戏结束界面
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {

        gameOverUI.SetActive(true);

        Invoke("RestartGame", restartDelay);

    }

    private void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        CCTVPlayerDetection.isGameOver = false;


    }
}