using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float gamePlayingTimer;
    private int enemyKills;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if(EndlessSurvivalManager.Instance.IsGamePlaying())
        {
            gamePlayingTimer += Time.deltaTime;
        }
    }

    public float TimePlaying()
    {
        return gamePlayingTimer;
    }

    public void IncKillCount()
    {
        enemyKills++;
    }

    public int KillCount()
    {
        return enemyKills;
    }
}
