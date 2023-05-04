using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndlessSurvivalManager : MonoBehaviour
{

    public static EndlessSurvivalManager Instance { get; private set; }

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
    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
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
}
