using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurvivalDayUI : MonoBehaviour
{
    public static SurvivalDayUI Instance { get; private set; }

    [SerializeField] private Image timerOfDay;
    [SerializeField] private TextMeshProUGUI survivalDay;
    [SerializeField] private Image endDayTime;
    [SerializeField] private Image dayTime;
    private enum State
    {
        NormalDay,
        LongNight,
        NightMare,
        LongNightMare
    }

    private State state;
    private int nightMareDay = 7;
    private int longNightDay = 3;
    private float normalDay = .5f;
    private float normalBeginNight = .75f;
    private float longBeginNight = .6f;
        
    private void Awake()
    {
        Instance = this;
        timerOfDay.fillAmount = 0f;
        dayTime.fillAmount = normalDay;
        endDayTime.fillAmount = normalBeginNight;
    }
    private void Start()
    {        
        EndlessSurvivalManager.Instance.OnEndingDay += EndlessSurvivalManager_OnEndingDay;
    }

    private void EndlessSurvivalManager_OnEndingDay(object sender, System.EventArgs e)
    {
        int nextDay = EndlessSurvivalManager.Instance.GetSurvivalDay();
        survivalDay.text = "DAY " + nextDay;
        if(nextDay % nightMareDay == 0 && nextDay % longNightDay == 0)
        {
            dayTime.fillAmount = normalDay;
            endDayTime.fillAmount = longBeginNight;
            state = State.LongNightMare;
        } else if (nextDay % longNightDay == 0 && nextDay % nightMareDay != 0)
        {
            dayTime.fillAmount = normalDay;
            endDayTime.fillAmount = longBeginNight;
            state = State.LongNight;
        } else if (nextDay % nightMareDay == 0 && nextDay % longNightDay != 0 )
        {
            dayTime.fillAmount = normalDay;
            endDayTime.fillAmount = normalBeginNight;
            state = State.NightMare;
        } else
        {
            dayTime.fillAmount = normalDay;
            endDayTime.fillAmount = normalBeginNight;
            state = State.NormalDay;
        }
    }

    public bool NormalDay()
    {
        return state == State.NormalDay;
    }

    public bool LongNight()
    {
        return state == State.LongNight;
    }

    public bool NormalNightMare()
    {
        return state == State.NightMare;
    }

    public bool LongNightMare()
    {
        return state == State.LongNightMare;
    }

    private void Update()
    {
        timerOfDay.fillAmount = EndlessSurvivalManager.Instance.GetDayTimerNormalized();
    }

    public float HalfDayToSecond()
    {
        return EndlessSurvivalManager.Instance.DayTimer() * normalDay / 2;
    }
    public float BeforeNormalNightToSecond()
    {
        return EndlessSurvivalManager.Instance.DayTimer() * (normalBeginNight - normalDay);
    }

    public float BeforeLongNightToSecond()
    {
        return EndlessSurvivalManager.Instance.DayTimer() * (longBeginNight - normalDay);
    }
    public float HalfNormalNightToSecond()
    {
        return EndlessSurvivalManager.Instance.DayTimer() * (1 - normalBeginNight) / 2;
    }

    public float HalfLongNightToSecond()
    {
        return EndlessSurvivalManager.Instance.DayTimer() * (1 - longBeginNight) / 2;
    }
}
