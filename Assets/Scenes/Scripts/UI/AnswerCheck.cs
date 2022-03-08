using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AnswerCheck : MonoBehaviour
{
    public string CorrectAnswer = "700";
    public InputField input;
    public GameObject quest;

    public IEnumerator UpdateScore() {

        WWWForm form = new WWWForm();

        form.AddField("score", Score.score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/holes/UcmGameWeb/web/index.php?action=update_score", form)) {
            yield return www.SendWebRequest();
        }
    }

    public void OnClick ()
    {
        if (input.text == CorrectAnswer)
        {
            if (HealthMonitor.HealthValue < 3) HealthMonitor.HealthValue += 1;
            Score.score += 100;
    
            StartCoroutine(this.UpdateScore());
        }
        else
        {
           HealthMonitor.HealthValue -= 1; 
        }

        quest.SetActive(false);
    }
}
