using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToThink = 20f;
    [SerializeField] float timeToRevealAnswer = 10f;

    public bool isAnsweringQuestion; // = true;

    // k gán j, mặc định vừa vào = 0 luôn;
    float timerValue;

    void Start()
    {
        timerValue = timeToThink;
        isAnsweringQuestion = true;
        Debug.Log(timerValue);
    }

    void Update()
    {

        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // Nếu TimerValue = 0 và đang true AnsweringTime(from start) 
        // thì chuyển thành thời gian hiện đáp án
        // hiện đáp án xong hết thời gian thì lại quay về hiện tgian suy nghĩ
        if (isAnsweringQuestion && timerValue <= 0)
        {
            isAnsweringQuestion = false;
            timerValue = timeToRevealAnswer;
        }

        // Nếu timerValue = 0 thì gán lại thời gian 1 câu vào
        // vừa vào là timerValue k có giá trị
        // j -> auto = 0, từ đó tiếp tục được gán timeToThink vào
        else if (timerValue <= 0 && isAnsweringQuestion == false)
        {
            timerValue = timeToThink;
            isAnsweringQuestion = true;
        }

        Debug.Log(timerValue);
        Debug.Log(isAnsweringQuestion);
    }


}
