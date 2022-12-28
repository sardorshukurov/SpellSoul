using UnityEngine;
public class PauseLogic : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;  
        GameIsPaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
