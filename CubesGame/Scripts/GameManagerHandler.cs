using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManagerHandler : MonoBehaviour
{
    public static GameManagerHandler Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private bool isGamePaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More then one game manager Instance");
        }
        Instance = this;

    }

    private void Start()
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);

        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        Health.OnPlayerDie += Health_OnPlayerDie;
    }

    private void Health_OnPlayerDie(object sender, EventArgs e)
    {
        SceneManager.LoadScene(0);
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return isGamePaused;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }

    }
}
