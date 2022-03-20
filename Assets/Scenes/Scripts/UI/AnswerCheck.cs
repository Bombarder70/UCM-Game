using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AnswerCheck : MonoBehaviour {
    public InputField input;
    public GameObject quest;

		public class Odpoved {
			public int id;
			public string odpoved;
			public int typ;
		}

    public IEnumerator UpdateScore() {

			WWWForm form = new WWWForm();

			form.AddField("score", Score.score);

			using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/holes/UcmGameWeb/web/index.php?action=update_score", form)) {
				yield return www.SendWebRequest();
			}
    }

    public void OnClick () {

			// Prejdi odpovede ak sa tam nachadza tak true
			foreach (string odpoved in ShowQuest.questAnswers) {
				if (input.text == odpoved) {
					if (HealthMonitor.HealthValue < 3) HealthMonitor.HealthValue += 1;
					Score.score += 100;

					StartCoroutine(this.UpdateScore());
					Debug.Log("Spravna odpoved");
					break;
				} else {
					HealthMonitor.HealthValue -= 1; 
					Debug.Log("Nespravna odpoved");
				}
			}

			quest.SetActive(false);
    }
}
