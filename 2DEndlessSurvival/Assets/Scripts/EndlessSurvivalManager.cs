using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndlessSurvivalManager : MonoBehaviour
{

    public static EndlessSurvivalManager Instance { get; private set; }

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
    private float waitingToStartTimer = 1f;
    private float gamePlayingTimer;
    private float dayTimerNow;
    private float dayTimerMax = 60f;
    private int survivalDay;
    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        dayTimerNow = dayTimerMax;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {                    
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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

    public float GetDayTimerNormalized()
    {
        return 1 - (dayTimerNow / dayTimerMax);
    }

    public int GetSurvivalDay()
    {
        return survivalDay;
    }
}
