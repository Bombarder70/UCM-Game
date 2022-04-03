using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShowQuest : MonoBehaviour {
	public GameObject quest;

	public Text otazkaText;
	public InputField odpovedText;
	public TextAsset jsonFile;

	public static List<string> questAnswers = new List<string>(); 
	public static int questId;

	public static string jsonFromDB;

	// Momentalny objekt napr. flasa_quest pre drestroy po zobrazeni
	public static GameObject questObject;

	[System.Serializable]
	public class Quests {
		public Quest[] quests;
	}

	// Drag and drop typ
	[System.Serializable]
	public class Presun {
		public int id;
		public string text;
		public int odpoved_id; // id spravnej odpovede
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
		string playerNickname = PlayerManager.nickname;

		using (UnityWebRequest www = UnityWebRequest.Get("https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=get_quests&playerNickname=" + playerNickname)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log("Otazky nenacitalo z databazy!");
				Quests questsInJson = JsonUtility.FromJson<Quests>(jsonFile.text);
				this.parseTextFromDB(questsInJson);
			} else {
				Debug.Log("Otazky nacitane z databazy");
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
		odpovedText.text = ""; //Vyresetuj odpoved
		StartCoroutine(this.loadJsonFromDB());
	}

	void OnTriggerEnter () {
		quest.SetActive (true);

		ShowQuest.questObject = gameObject;
		this.loadQuestFromJson();
	}
}
