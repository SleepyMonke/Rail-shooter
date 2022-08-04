using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKepper : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    void Start() 
    {
      scoreText = GetComponent<TMP_Text>();  
    }
    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text= score.ToString();
    }
}
