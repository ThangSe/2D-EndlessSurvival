using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSurvivalManager : MonoBehaviour
{

    public static EndlessSurvivalManager Instance { get; private set; }
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
                if (waitingToStartTimer < 0f) state = State.GamePlaying;
                break;
            case State.GamePlaying:
                gamePlayingTimer += Time.deltaTime;
                if (Player.Instance.IsDeath()) state = State.GameOver;
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
