using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;


    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Slider")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    [Header("NextQuestion")]
    [SerializeField] GameObject nextQuestionButton;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timer = FindObjectOfType<Timer>();

        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

        //nextQuestionButton.SetActive(false);
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            // ấn trả lời phát chạy hết thời gian và chuyển sang thời gian hiện đáp án luôn
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            // thời gian hết mà k trả lời thì tự truyền vào index sai và k trùng index đáp án
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

       // nextQuestionButton.SetActive(true);

        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        // nếu chọn đáp án có index trùng với index đáp án đúng thì đổi sprite và hiện ra chúc mừng
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Amazed me everytime, you smart cookies!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        // chọn index khác thì hiện đáp án đúng ra và đổi sprite đáp án đúng
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry dear, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            // set state interactable của button
            SetButtonState(true);

            // chuyển các button về sprite mặc định khi shift qua câu hỏi mới
            SetDefaultButtonSprites();

            GetRandomQuestion();
            DisplayQuestion();

            progressBar.value++;

            //nextQuestionButton.SetActive(false);
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        // làm việc với list thông qua contains và remove
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    // Khi ấn vào nút nextQuestion
    //public void OnNextQuestionButtonSelected()
    //{
    //    GetNextQuestion();
    //    timer.CancelTimer();
    //    nextQuestionButton.SetActive(false);

    //}
}
