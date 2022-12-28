using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public float restartDelay = 1f;

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        ChangeGameState();
        Debug.Log("Game Over");
        Invoke("Restart", restartDelay);
    }
    private void ChangeGameState()
    {
        if (gameHasEnded)
        {
            gameHasEnded = false;
        }
        else
        {
            gameHasEnded = true;
        }
        
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel(int numberOfScene)
    {
        SceneManager.LoadScene(numberOfScene + 1);
    }
}
