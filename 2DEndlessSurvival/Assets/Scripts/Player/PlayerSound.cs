using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footSteepTimer;
    private float footsteepTimerMax = .2f;
    private int stepNumReset = 0;
    private int walkingNum;
    private int RunningNum;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footSteepTimer -= Time.deltaTime;
        if (footSteepTimer < 0f)
        {            
            float volume = 1f;
            footSteepTimer = footsteepTimerMax;
            if(!player.IsRunning() && player.IsWalking() && player.IsGrounded() && PlayerAnimations.Instance.canMove())
            {
                SoundManager.Instance.PlayWalkingSound(player.transform.position, walkingNum, volume);
                walkingNum++;
                if (walkingNum > 1) walkingNum = stepNumReset;
            }
            if (player.IsRunning() && player.IsGrounded() && PlayerAnimations.Instance.canMove())
            {
                SoundManager.Instance.PlayRunningSound(player.transform.position, RunningNum, volume);
                RunningNum++;
                if (RunningNum > 1) RunningNum = stepNumReset;
            }
        }
    }
}
