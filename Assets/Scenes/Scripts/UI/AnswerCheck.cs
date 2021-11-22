using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCheck : MonoBehaviour
{
    public string CorrectAnswer = "700";
    public InputField input;
    public GameObject quest;

    public void OnClick ()
    {
        if (input.text == CorrectAnswer)
        {
            if (HealthMonitor.HealthValue < 3) HealthMonitor.HealthValue += 1;
            Score.score += 100;
        }
        else
        {
           HealthMonitor.HealthValue -= 1; 
        }

        quest.SetActive(false);
    }
}
