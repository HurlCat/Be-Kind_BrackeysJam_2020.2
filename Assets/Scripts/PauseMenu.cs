using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private string _menuScene;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if(GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        // bring up pause
        _pauseUI.SetActive(true);
        
        // freeze game
        Time.timeScale = 0f;
        // set bool
        GameIsPaused = true;
        
        GameController.singleton.ToggleFilter();
    }
    
    public void Resume()
    {
        _pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        GameController.singleton.ToggleFilter();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_menuScene);
        
        GameController.singleton.ToggleFilter();
    }
    
    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        GameController.singleton.ToggleFilter();
    }

    public void QuitGame() => Application.Quit();
}
