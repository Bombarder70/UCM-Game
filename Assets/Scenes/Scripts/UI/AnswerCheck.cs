using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCheck : MonoBehaviour
{
    public string CorrectAnswer = "700";
    public InputField input;
    public GameObject quest;

    public IEnumerator UpdateScore() {
        WWWForm form = new WWWForm();

        //form.AddField("nickname", "Pirat2");
        form.AddField("score", Score.score);

        WWW www = new WWW(
            "http://localhost/holes/UcmGameWeb/index.php?action=update_score.php",
            form
        );

        yield return www;
        print(www.text);
    }

    public void OnClick ()
    {
        if (input.text == CorrectAnswer)
        {
            if (HealthMonitor.HealthValue < 3) HealthMonitor.HealthValue += 1;
            Score.score += 100;

            this.UpdateScore();
        }
        else
        {
           HealthMonitor.HealthValue -= 1; 
        }

        quest.SetActive(false);
    }
}
