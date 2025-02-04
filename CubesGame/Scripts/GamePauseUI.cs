using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;


    private void Awake()
    {

        resumeButton.onClick.AddListener(() =>
        {
            GameManagerHandler.Instance.TogglePauseGame();
        });

        //mainMenuButton.onClick.AddListener(() =>
        //{
        //    Loader.Load(Loader.Scene.MainMenuScene);
        //});

        //optionsButton.onClick.AddListener(() =>
        //{
        //    OptionsUI.Instance.Show();
        //});
    }

    private void Start()
    {
        GameManagerHandler.Instance.OnGamePaused += GameManagerHandler_OnGamePaused;
        GameManagerHandler.Instance.OnGameUnpaused += GameManagerHandler_OnGameUnpaused;

        Hide();
    }

    private void GameManagerHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManagerHandler_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
