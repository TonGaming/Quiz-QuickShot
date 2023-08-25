using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionsSO> questions;
    QuestionsSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;


    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    // biến để thay đổi sprite của button (vàng + xanh)
    Image buttonImage;
    bool state = true;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        Debug.Log("bdau chay: " + state);
        GetNextQuestion();
        SetButtonState(true);
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        // nối fillFraction của Timer.cs với FillAmount của Image(Image cũng được nối trong inspector)
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            /* k thể truyền vào từ 0 tới 3 vì có khả năng sẽ trùng với correctAnswerIndex
             * vì thế ta sẽ truyền vào -1 để index đc truyền vào luôn luôn lệch với index của dáp án đúng
             * và sẽ luôn hiện ra thông báo: b đã sai, đáp án đúng là abcxyz chứ kp là b đã chọn đúng đáp án 
             */
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);

        // set giá trị cho hasAnsweredEarly
        hasAnsweredEarly = true;

        state = false;
        SetButtonState(state);

        // chon dap an thi khoa lai
        timer.CancelTimer();
        //SetButtonState(false);

        // Khoá answerButton sau khi chọn đáp án mỗi câu
        //for (int i = 0; i < answerButtons.Length; i++)
        //{
        //    Button button = answerButtons[i].GetComponent<Button>();
        //    button.interactable = state;
        //}
    }

    private void DisplayAnswer(int index)
    {
        // khi click vào button sẽ có index báo về
        // nếu index đó = với index của đáp án thì oke easy
        // chỉ cần tạo biến để thay sprite thoi
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Your answer is correct";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            //questionText.text = "Your answer is incorrect! The correct answer was: " 
            //    // Track tới đáp án đúng dựa vào index của đáp án
            //    + answerButtons[correctAnswerIndex]; sai vì chưa gán correctAnswerIndex
            //buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            //buttonImage.sprite = correctAnswerSprite;

            // gán giá trị cho biến correctAnswerIndex
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            // tạo biến trung gian để nối vào questionText.text
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry dear, The correct answer was:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        DisplayQuestion();
        SetDefaultButtonSprite();
        state = true;
        SetButtonState(true);
    }
    void DisplayQuestion()
    {
        /* truy cập vào text của questiontext, gán cho nó giá trị của 
        * currentQuestion, giá trị của currentQuestion thì truy cập tới kết quả
        * currentQuestion của hàm getquestion() ở questionsSO */
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    // cho phép click vào các phím khi state là true
    // false thì khoá hết thông qua việc tắt interactable
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
