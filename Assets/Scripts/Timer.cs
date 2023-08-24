using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] float timeToThink = 30f;
    [SerializeField] float timeToRevealAnswer = 5f;

    public bool isAnsweringQuestion; // default is false;
    public bool loadNextQuestion; // default is false;
    public float fillFraction;  // Timer fraction reducing

    
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

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        // nếu thời gian đang chạy, đang trong tgian suy nghĩ thì timerValue sẽ chia cho tgian snghi
        if (isAnsweringQuestion && timerValue > 0)
        {
            // 30/30 = 1 (fill 100%), 10/30 = 0.33... (fill 30%) and so on
            fillFraction = timerValue / timeToThink;
           

        }
        // 
        else if (isAnsweringQuestion == false && timerValue > 0)
        {
            fillFraction = timerValue / timeToRevealAnswer;
          
        }

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
            loadNextQuestion = true;
        }

        Debug.Log(isAnsweringQuestion + ": " + timerValue + " / " + timeToThink + " = "+ fillFraction);
    }

    
}
