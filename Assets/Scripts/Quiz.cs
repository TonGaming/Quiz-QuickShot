using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Quiz : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionsSO question;

    private void Start()
    {
        /* truy cập vào text của questionText, gán cho nó giá trị của 
        * question, giá trị của question thì truy cập tới kết quả
        * question của hảm GetQuestion() ở QuestionsSO */
        questionText.text = question.GetQuestion();
    }
}
