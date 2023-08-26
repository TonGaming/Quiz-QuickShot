using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionTotal = 10;
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;

    }

    public int CalculateScore()
    {
        // lấy câu hỏi trả lời đúng / cho tổng số câu: 3 câu đúng / 10 câu tổng = .3 * 100 = 30%
        return Mathf.RoundToInt( correctAnswers / (float)questionTotal * 100);
    }
}
