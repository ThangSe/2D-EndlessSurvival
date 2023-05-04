using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;

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
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
