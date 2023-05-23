using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndlessSurvivalManager : MonoBehaviour
{

    public static EndlessSurvivalManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnEndingDay;
    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        GamePlaying,
        GamePause,
        GameOver,
    }

    private State state;
    private float gamePlayingTimer;
    private float dayTimerNow;
    private float dayTimerMax = 30f;
    private int survivalDay;
    private bool isGamePaused = false;
    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        dayTimerNow = dayTimerMax;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameOverUI.Instance.OnRespawnAction += GameOver_OnRespawnAction;
        GameInput.Instance.OpenTutorialMenu += GameInput_OpenTutorialMenu;
    }

    private void GameInput_OpenTutorialMenu(object sender, EventArgs e)
    {
        state = State.GamePlaying;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameOver_OnRespawnAction(object sender, EventArgs e)
    {
        state = State.GamePlaying;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.GamePlaying:
                gamePlayingTimer += Time.deltaTime;
                dayTimerNow -= Time.deltaTime;
                if(dayTimerNow <= 0)
                {
                    survivalDay++;
                    OnEndingDay?.Invoke(this, EventArgs.Empty);
                    dayTimerNow = dayTimerMax;
                }
                if (Player.Instance.IsDeath())
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePause:
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsGamePause()
    {
        return state == State.GamePause;
    }

    public float GetDayTimerNormalized()
    {
        return 1 - (dayTimerNow / dayTimerMax);
    }

    public int GetSurvivalDay()
    {
        return survivalDay;
    }

    public float DayTimer()
    {
        return dayTimerMax;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            state = State.GamePause;
        } else
        {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            state = State.GamePlaying;
        }
    }
}
