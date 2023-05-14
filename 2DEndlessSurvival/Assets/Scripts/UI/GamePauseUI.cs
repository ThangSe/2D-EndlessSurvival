using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            EndlessSurvivalManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        EndlessSurvivalManager.Instance.OnGamePaused += EndlessSurvivalManager_OnGamePaused;
        EndlessSurvivalManager.Instance.OnGameUnpaused += EndlessSurvivalManager_OnGameUnpaused;

        Hide();
    }

    private void EndlessSurvivalManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void EndlessSurvivalManager_OnGamePaused(object sender, System.EventArgs e)
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
