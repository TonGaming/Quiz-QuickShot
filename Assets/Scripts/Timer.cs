using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 20f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;
    
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        // nếu đang trả lời cau hỏi mà hết tg -> tgian hiện đáp án
        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                // timer sẽ được chia dựa theo từng frame, phép chia này sẽ dựa theo thời gian trong inspector
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        // nếu đang k trả lời câu hỏi mà còn thời gian -> chuyển thành tgian trả lời câu hỏi và load câu hỏi mới vào
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
