using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float gamePlayingTimer;
    private int enemyKills;
    private int totalCopperCoin;
    private int totalSilverCoin = 2;
    private int totalGoldenCoin = 1;
    [SerializeField] TextMeshProUGUI textCropperCoin;
    [SerializeField] TextMeshProUGUI textSilverCoin;
    [SerializeField] TextMeshProUGUI textGoldenCoin;
    private void Start()
    {
        Instance = this;
        textCropperCoin.SetText(totalCopperCoin.ToString());
        textSilverCoin.SetText(totalSilverCoin.ToString());
        textGoldenCoin.SetText(totalGoldenCoin.ToString());
        Player.Instance.OnCoinChanged += Player_OnCoinChanged;
        GameOverUI.Instance.OnRespawnAction += GameOver_OnRespawnAction;
    }

    private void GameOver_OnRespawnAction(object sender, System.EventArgs e)
    {
        totalSilverCoin--;
        textSilverCoin.SetText(totalSilverCoin.ToString());
    }

    private void Player_OnCoinChanged(object sender, Player.OnCoinChangedEventArgs e)
    {
        if(e.typeCoin == "CopperCoin")
        {
            totalCopperCoin += e.amountCoin;
            textCropperCoin.SetText(totalCopperCoin.ToString());
        }
        if (e.typeCoin == "SilverCoin")
        {
            totalSilverCoin += e.amountCoin;
            textSilverCoin.SetText(totalSilverCoin.ToString());
        }
        if (e.typeCoin == "GoldenCoin")
        {
            totalGoldenCoin += e.amountCoin;
            textGoldenCoin.SetText(totalGoldenCoin.ToString());
        }
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

    public int GetTotalCropperCoin()
    {
        return totalCopperCoin;
    }

    public int GetTotalSilverCoin()
    {
        return totalSilverCoin;
    }

    public int GetTotalGoldenCoin()
    {
        return totalGoldenCoin;
    }
}
