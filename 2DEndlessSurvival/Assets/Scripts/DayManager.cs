using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DayManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField] private Image endDayTime;
    [SerializeField] private Image dayTime;
    [SerializeField] private Image timerOfDay;
    [SerializeField] private EnemiesSO[] enemiesSO;

    private float lightAtEndDay = .5f;
    private float lightAtBeginNight = .3f;
    private float lightAtMidNight = .1f;
    private float lightAtBeginDay = .5f;
    private float lightAtMidday = 1.5f;

    private float spawnRate = 1f;
    private float spawnTime;

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
        spawnTime = spawnRate;
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
                        Player.Instance.LightOn();
                        state = State.NightTime;
                        Debug.Log("Begin Night");
                    }
                    break;
                case State.NightTime:
                    if(spawnTime >= spawnRate)
                    {
                        Instantiate(enemiesSO[Random.Range(0, enemiesSO.Length)].prefab, GetRandomSpawnPostion(), Quaternion.identity);
                        spawnTime = 0f;
                        Debug.Log("New enemies");
                    }
                    spawnTime += Time.deltaTime;
                    if (timerOfDay.fillAmount >=0 && timerOfDay.fillAmount < dayTime.fillAmount)
                    {
                        Player.Instance.LightOff();
                        globalLight.intensity = .5f;
                        state = State.DayTime;
                    }
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if(EndlessSurvivalManager.Instance.IsGamePlaying())
        {          
            switch(state)
            {
                case State.DayTime:
                    if (timerOfDay.fillAmount < dayTime.fillAmount / 2) 
                        globalLight.intensity += Time.fixedDeltaTime * (lightAtMidday - lightAtBeginDay) / SurvivalDayUI.Instance.HalfDayToSecond();
                    if (timerOfDay.fillAmount >= dayTime.fillAmount / 2) 
                        globalLight.intensity -= Time.fixedDeltaTime * (lightAtMidday - lightAtEndDay) / SurvivalDayUI.Instance.HalfDayToSecond();
                    break;
                case State.EndDayTime:
                    if(SurvivalDayUI.Instance.NormalDay() || SurvivalDayUI.Instance.NormalNightMare()) 
                        globalLight.intensity -= Time.fixedDeltaTime * (lightAtEndDay - lightAtBeginNight) / SurvivalDayUI.Instance.BeforeNormalNightToSecond();
                    if(SurvivalDayUI.Instance.LongNight() || SurvivalDayUI.Instance.LongNightMare()) 
                        globalLight.intensity -= Time.fixedDeltaTime * (lightAtEndDay - lightAtBeginNight) / SurvivalDayUI.Instance.BeforeLongNightToSecond();
                    break;
                case State.NightTime:
                    if(SurvivalDayUI.Instance.NormalDay() || SurvivalDayUI.Instance.NormalNightMare())
                    {
                        if (timerOfDay.fillAmount - endDayTime.fillAmount < (1 - endDayTime.fillAmount) / 2) 
                            globalLight.intensity -= Time.fixedDeltaTime * (lightAtBeginNight - lightAtMidNight) / SurvivalDayUI.Instance.HalfNormalNightToSecond();
                        if (timerOfDay.fillAmount - endDayTime.fillAmount >= (1 - endDayTime.fillAmount) / 2)
                            globalLight.intensity += Time.fixedDeltaTime * (lightAtBeginDay - lightAtMidNight) / SurvivalDayUI.Instance.HalfNormalNightToSecond();
                    }
                    if (SurvivalDayUI.Instance.LongNight() || SurvivalDayUI.Instance.LongNightMare())
                    {
                        if (timerOfDay.fillAmount - endDayTime.fillAmount < (1 - endDayTime.fillAmount) / 2)
                            globalLight.intensity -= Time.fixedDeltaTime * (lightAtBeginNight - lightAtMidNight) / SurvivalDayUI.Instance.HalfLongNightToSecond();
                        if (timerOfDay.fillAmount - endDayTime.fillAmount >= (1 - endDayTime.fillAmount) / 2) 
                            globalLight.intensity += Time.fixedDeltaTime * (lightAtBeginDay - lightAtMidNight) / SurvivalDayUI.Instance.HalfLongNightToSecond();
                    }
                    
                    break;
            }
        }
    }

    private Vector3 GetRandomSpawnPostion()
    {
        float spwanDistanceMax = 30f;
        float spwanDistanceMin = 20f;
        Vector3 playerPosition = Player.Instance.GetPosition();
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0).normalized;
        if (playerPosition.x < spwanDistanceMax) randomDir = new Vector3(Random.Range(0f, 1f), 0).normalized;
        return playerPosition + randomDir * Random.Range(spwanDistanceMax, spwanDistanceMin);
    }

}
