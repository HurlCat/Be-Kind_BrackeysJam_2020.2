using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameSettings
{
    public int rewindProbability;
    internal int minVHSBinStock, maxVHSBinStock;
    public float VHSBinRefillRateInSeconds;
    
    internal float minCustomerSpeed, maxCustomerSpeed;
    internal float minCustomerBrowse, maxCustomerBrowse;
    internal float minCustomerWait, maxCustomerWait;

    internal float minCustomerSpawnRate, maxCustomerSpawnRate;
    public float increaseSpawnRate;

    public int GetRandVHSBinStock() => UnityEngine.Random.Range(minVHSBinStock, maxVHSBinStock);
    public float GetRandCustomerSpeed() => UnityEngine.Random.Range(minCustomerSpeed, maxCustomerSpeed);

    public Vector2 GetCustomerSpawnRate()
    {
        return new Vector2(
            minCustomerSpawnRate,
            maxCustomerSpawnRate
            );
    }
    
    public Vector2 GetRandCustomerStats()
    {
        return new Vector3(
            UnityEngine.Random.Range(minCustomerBrowse, maxCustomerBrowse),
            UnityEngine.Random.Range(minCustomerWait, maxCustomerWait));
    }
    
}

public class GameController : MonoBehaviour
{
    public static GameController singleton;
    public static bool isGameOver = false;

    private GameObject _gameOverUI;
    public GameObject _tutorialSystem;

    public bool tutorialsEnabled = true;
    
    public float gameTimer = 120f;
    public int dayCount = 0;
    
    public GameSettings settings = new GameSettings()
    {
        rewindProbability = 33, minVHSBinStock = 15, maxVHSBinStock = 25,
        VHSBinRefillRateInSeconds = 5f, minCustomerSpeed = 1f, maxCustomerSpeed = 3f, minCustomerBrowse = 4f,
        maxCustomerBrowse = 9f, minCustomerWait = 4f, maxCustomerWait = 7f, minCustomerSpawnRate = 7f,
        maxCustomerSpawnRate = 20f, increaseSpawnRate = 20f
    };
    
    public Camera _sceneCamera;

    private void Awake()
    {
        if (singleton != null)
            Destroy(this);
        else
        {
            singleton = this;         
            DontDestroyOnLoad(this);
        }


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            _sceneCamera = Camera.main;
            
            if (tutorialsEnabled)
                Invoke(nameof(StartTutorial), 1.5f);

            _gameOverUI = ScoreKeeper.Singleton.gameOverUI;
        }
    }

    void StartTutorial() => Instantiate(_tutorialSystem);

    public void SetDifficulty(float difficulty)
    {
        if (difficulty > 6) // hard
        {
            Debug.Log("HARD");
            settings = new GameSettings()
            {
                rewindProbability = 50, minVHSBinStock = 15, maxVHSBinStock = 20,
                VHSBinRefillRateInSeconds = 5f, minCustomerSpeed = 1.5f, maxCustomerSpeed = 4f, minCustomerBrowse = 4f,
                maxCustomerBrowse = 9f, minCustomerWait = 4f, maxCustomerWait = 7f, minCustomerSpawnRate = 5f,
                maxCustomerSpawnRate = 15f, increaseSpawnRate = 15f
            };
        }
        else if (difficulty > 3) // normal
        {
            Debug.Log("Normal");
            settings = new GameSettings()
            {
                rewindProbability = 40, minVHSBinStock = 15, maxVHSBinStock = 20,
                VHSBinRefillRateInSeconds = 5f, minCustomerSpeed = 1.5f, maxCustomerSpeed = 3.5f, minCustomerBrowse = 4f,
                maxCustomerBrowse = 8f, minCustomerWait = 4f, maxCustomerWait = 7f, minCustomerSpawnRate = 5f,
                maxCustomerSpawnRate = 20f, increaseSpawnRate = 15f
            };
        }
        else // easy
        {
            Debug.Log("EASY");
            settings = new GameSettings()
            {
                rewindProbability = 33, minVHSBinStock = 15, maxVHSBinStock = 25,
                VHSBinRefillRateInSeconds = 5f, minCustomerSpeed = 1f, maxCustomerSpeed = 3f, minCustomerBrowse = 4f,
                maxCustomerBrowse = 9f, minCustomerWait = 4f, maxCustomerWait = 7f, minCustomerSpawnRate = 7f,
                maxCustomerSpawnRate = 20f, increaseSpawnRate = 20f
            };
        }
    }

    public void TogglePause()
    {
        PauseMenu.GameIsPaused = !PauseMenu.GameIsPaused;
        // freeze game
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
    }

    public void GameOver()
    {
        _gameOverUI.SetActive(true);
        isGameOver = true;
        
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
        ToggleFilter();
    }

    public void ToggleFilter()
    {
        VideoPlayer videoPlayer = _sceneCamera.GetComponent<VideoPlayer>();
        VHSPostProcessEffect post = _sceneCamera.GetComponent<VHSPostProcessEffect>();
        AudioSource audio = _sceneCamera.GetComponent<AudioSource>();

        videoPlayer.enabled = !videoPlayer.enabled;
        post.enabled = !post.enabled;

        audio.pitch = audio.pitch == 1f ? 0f : 1f;
    }
}