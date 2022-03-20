using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShowQuest : MonoBehaviour {
	public GameObject quest;

	public Text otazkaText;
	public TextAsset jsonFile;

	public static List<string> questAnswers = new List<string>(); 
	public static int questId;

	public static string jsonFromDB;

	[System.Serializable]
	public class Quests {
		public Quest[] quests;
	}

	[System.Serializable]
	public class Odpoved {
		public int id;
		public string odpoved;
		public int typ;
	}

	[System.Serializable]
	public class Quest {
		public int id;
		public string otazka;
		public int typ;
		public Odpoved[] odpovede;
		public bool zobrazena;
		public bool odpoved;
	}

	public IEnumerator loadJsonFromDB() {
		using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/holes/UcmGameWeb/web/index.php?action=get_quests")) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log("Otazky nenacitalo z databazy!");
				Quests questsInJson = JsonUtility.FromJson<Quests>(jsonFile.text);
				this.parseTextFromDB(questsInJson);
			} else {
				Quests questsInJson = JsonUtility.FromJson<Quests>(www.downloadHandler.text);
				this.parseTextFromDB(questsInJson);
			}

		}
	}

	public void parseTextFromDB(Quests questsInJson) {
		foreach (Quest quest in questsInJson.quests) {
			if (quest.zobrazena == false) {
				otazkaText.text = quest.otazka;
				ShowQuest.questId = quest.id;

				// Nahraj vsetky mozne spravne odpovede
				foreach (Odpoved odpoved in quest.odpovede) {
					ShowQuest.questAnswers.Add(odpoved.odpoved);
				}

				break;
			}
		}
	}

	void loadQuestFromJson() {
		StartCoroutine(this.loadJsonFromDB());
	}

	void OnTriggerEnter () {
		quest.SetActive (true);

		this.loadQuestFromJson();
	}
}
