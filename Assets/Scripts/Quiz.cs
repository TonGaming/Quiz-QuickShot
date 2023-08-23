using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Quiz : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionsSO question;
    [SerializeField] GameObject[] answerButtons;

    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    // biến để thay đổi sprite của button (vàng + xanh)
    Image buttonImage;
    bool state = true;

    void Start()
    {
        Debug.Log("bdau chay: " + state);
        GetNextQuestion();
        SetButtonState(true);

    }

    void Update()
    {

    }
    public void OnAnswerSelected(int index)
    {

        // khi click vào button sẽ có index báo về
        // nếu index đó = với index của đáp án thì oke easy
        // chỉ cần tạo biến để thay sprite thoi
        if (index == question.GetCorrectAnswerIndex())
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
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            // tạo biến trung gian để nối vào questionText.text
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry dear, The correct answer was:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        state = false;
        SetButtonState(state);
        //SetButtonState(false);

        // Khoá answerButton sau khi chọn đáp án mỗi câu
        //for (int i = 0; i < answerButtons.Length; i++)
        //{
        //    Button button = answerButtons[i].GetComponent<Button>();
        //    button.interactable = state;
        //}
    }

    void GetNextQuestion()
    {
        DisplayQuestion();
        SetDefaultButtonSprite();
        state = true;
    }
    void DisplayQuestion()
    {
        /* truy cập vào text của questiontext, gán cho nó giá trị của 
        * question, giá trị của question thì truy cập tới kết quả
        * question của hàm getquestion() ở questionsSO */
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
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
