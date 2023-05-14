using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    public event EventHandler OnRespawnAction;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button respawnButton;

    private void Awake()
    {
        Instance = this;
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        respawnButton.onClick.AddListener(() =>
        {
            OnRespawnAction?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start()
    {
        EndlessSurvivalManager.Instance.OnStateChanged += EndlessSurvivalManager_OnStateChanged;
        Hide();
    }

    private void EndlessSurvivalManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(EndlessSurvivalManager.Instance.IsGameOver())
        {
            Show();
            scoreText.text = ScoreManager.Instance.KillCount().ToString();
        } else
        {
            Hide();
        }
    }

    private void Update()
    {       
    }

    private void Show()
    {
        gameObject.SetActive(true);
        if(ScoreManager.Instance.GetTotalSilverCoin() <= 0)
        {
            transform.Find("RespawnButton").gameObject.SetActive(false);
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
