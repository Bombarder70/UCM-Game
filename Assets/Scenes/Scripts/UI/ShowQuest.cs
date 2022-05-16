using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShowQuest : MonoBehaviour {
	public float timeForAnswer;
	public GameObject image;
	GameObject pirat;
	public int flasa_id;
	public GameObject quest;

	public Text otazkaText;
	public InputField odpovedText;
	public TextAsset jsonFile;

	public static List<string> questAnswers = new List<string>(); 
	public static int questId;

	public static string jsonFromDB;

	// Momentalny objekt napr. flasa_quest pre drestroy po zobrazeni
	public static GameObject questObject;

	private Movement playerMovement;
	private Animator playerAnimator;
	private PlayerSettings playerSettings;

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

	[System.Serializable]
	public class Structure {
		public Quests quests;
	}

	[System.Serializable]
	public class DbResponse {
		public int idPlayerGenerator;
		public string structure;
	}

	void Start() {
		pirat = GameObject.FindWithTag("pirat");

		if (pirat != null) {
			this.playerMovement = pirat.GetComponent<Movement>();
			this.playerAnimator = pirat.GetComponent<Animator>();
			this.playerSettings = pirat.GetComponent<PlayerSettings>();
		}
	}

	public IEnumerator loadJsonFromDB() {
		string playerNickname = (PlayerManager.nickname != "" ? PlayerManager.nickname : "Pirat"); // Pozn. DEV MOD ak nieje nsatavene meno nacitaj Pirat

		using (UnityWebRequest www = UnityWebRequest.Get(
			"https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=get_quests&playerNickname=" + playerNickname
			+ "&idGenerator=" + PlayerManager.idGenerator
		)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log("Otazky nenacitalo z databazy!");
				Quests questsInJson = JsonUtility.FromJson<Quests>(jsonFile.text);
				this.parseTextFromDB(questsInJson);
			} else {
				Debug.Log("Otazky nacitane z databazy");
				
				DbResponse response = JsonUtility.FromJson<DbResponse>(www.downloadHandler.text);
				PlayerManager.idPlayerGenerator = response.idPlayerGenerator; // Nastav idPlayerGenerator
				Quests questsInJson = JsonUtility.FromJson<Quests>(response.structure);

				this.parseTextFromDB(questsInJson);
			}

		}
	}

	public void parseTextFromDB(Quests questsInJson) {
		foreach (Quest quest in questsInJson.quests) {
			if (quest.zobrazena == false && quest.id == flasa_id) {
				if (flasa_id == 6) image.SetActive(true);
				else image.SetActive(false);
				GameManager.questionTime = timeForAnswer;
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
		GameManager.timer = 0;
		GameManager.inQuest = true;
	//	GameManager.Instance.UpdateTime();
		quest.SetActive (true);

	//	pirat.GetComponent<Movement>().paused = true;
		StopEnemies();

		this.playerMovement.enabled = false;
		this.playerAnimator.enabled = false;
		this.playerSettings.getHitScreen.SetActive(false);

		ShowQuest.questObject = gameObject;
		this.loadQuestFromJson();
	}



	void StopEnemies()
	{

		List<GameObject> enemies = new List<GameObject> ();
 		enemies.AddRange(GameObject.FindGameObjectsWithTag("green_eyes_skeleton"));
		enemies.AddRange(GameObject.FindGameObjectsWithTag("red_eyes_skeleton"));
		enemies.AddRange(GameObject.FindGameObjectsWithTag("normal_skeleton"));

		foreach (var skeleton in enemies)
		{
			skeleton.GetComponent<EnemyController>().paused = true;
		}
	/*	GameObject[] enemies ;
             enemies = GameObject.FindGameObjectsWithTag("green_eyes_skeleton" || "a");
             foreach(GameObject lightuser in objs) {
                 lightuser.light.enabled=false;
             }*/
	}
}
