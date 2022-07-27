using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    float timeToAnswer;

    [SerializeField]
    float timeToShowAnswer;

    float timerMaxValue;

    public float timerValue;
    public float timerFraction;
    public bool isAnsweringQuestion;


    void UpdateTimer()
    {
       
        timerValue -= Time.deltaTime;

        if (timerValue > 0)
        {
            timerFraction = timerValue / timerMaxValue;
        }
       
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    public void ResetTimer()
    {
        timerValue = (isAnsweringQuestion) ? timeToAnswer: timeToShowAnswer;
        timerMaxValue = (isAnsweringQuestion) ? timeToAnswer : timeToShowAnswer;
    }

    void Update()
    {
        UpdateTimer();
    }
}
