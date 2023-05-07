using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [SerializeField] private Image endDayTime;
    [SerializeField] private Image dayTime;
    [SerializeField] private Image timerOfDay;

    private enum State
    {
        NightTime,
        EndDayTime,
        DayTime
    }

    private State state;

    private void Awake()
    {
        state = State.DayTime;
    }

    private void Update()
    {
        if(EndlessSurvivalManager.Instance.IsGamePlaying())
        {
            switch (state)
            {
                case State.DayTime:
                    if (timerOfDay.fillAmount >= dayTime.fillAmount)
                    {
                        if(SurvivalDayUI.Instance.NormalDay())
                        {
                            Debug.Log("Normal day");
                        }
                        if(SurvivalDayUI.Instance.LongNight())
                        {
                            Debug.Log("Long night");
                        }
                        if (SurvivalDayUI.Instance.NormalNightMare())
                        {
                            Debug.Log("Normal Night mare");
                        }
                        if (SurvivalDayUI.Instance.LongNightMare())
                        {
                            Debug.Log("Long night mare");
                        }
                        state = State.EndDayTime;
                        Debug.Log("End day");
                    }
                    break;
                case State.EndDayTime:
                    if (timerOfDay.fillAmount >= endDayTime.fillAmount)
                    {
                        state = State.NightTime;
                        Debug.Log("Begin Night");
                    }
                    break;
                case State.NightTime:
                    if (timerOfDay.fillAmount >=0 && timerOfDay.fillAmount < dayTime.fillAmount)
                    {
                        state = State.DayTime;
                        Debug.Log("End night");
                    }
                    break;
            }
        }
    }
}
