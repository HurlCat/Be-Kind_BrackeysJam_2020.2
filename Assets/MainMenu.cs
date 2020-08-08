using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _main, _settings;

    [SerializeField] private string _gameScene;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            QuitGame();
    }

    public void LoadGame() => SceneManager.LoadScene(_gameScene);
    
    public void QuitGame() => Application.Quit();

    public void ToggleMenuScreen()
    {
        _settings.SetActive(!_settings.activeSelf);
        _main.SetActive(!_main.activeSelf);
    }
}
