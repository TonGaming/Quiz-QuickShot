using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endScreen;

    
    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            
        }
    }

    public void onRestartLevel()
    {
        // Hồi còn ngây dại 
        // SceneManager.LoadScene("Quiz-Master");

        // Giờ đã khôn hơn - Lấy ra index của scene hiện tại 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
