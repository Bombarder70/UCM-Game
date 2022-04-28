using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

		private Button zmenaOtazokButton;
		private InputField zmenaOtazokInput;
		private Dropdown generatorDropdown;
		private Text error;
		private Text selectedGenerator;
		private Button zrusitOtazkyButton;

		// Player stats
		private Text playerScore;
		private Text deaths;
		private Text correctAnswers;
		private Text uncorrectAnswers;

		private GameObject zmenaOtazokButtonObject;
		private GameObject zmenaOtazokInputObject;
		private GameObject generatorDropdownObject;
		private GameObject errorObject;
		private GameObject zrusitOtazkyButtonObject;

		private int zmenaOtazokButtonStav = 1;

		[System.Serializable]
		public class Generator {
			public int id;
			public string name;
		}

		[System.Serializable]
		public class Generators {
			public string status;
			public int idRoom;
			public Generator[] data;
		}

		[System.Serializable]
		public class PlayerStats {
			public int id;
			public int idPlayerGenerator;
			public int score;
			public int deaths;
			public int correct_answers;
			public int uncorrect_answers;
		}

		[System.Serializable]
		public class PlayerData {
			public int idPlayer;
			public int idGenerator;
			public int idPlayerGenerator;
			public float lastPositionX;
			public float lastPositionY;
			public float lastPositionZ;
		}

		public int idRoom = 1;

    public void Start() {
      this.zmenaOtazokButton = GameObject.Find("ZmenaOtazokButton").GetComponent<Button>();
			this.zmenaOtazokInput = GameObject.Find("ZmenaOtazokInput").GetComponent<InputField>();
			this.generatorDropdown = GameObject.Find("GeneratorDropdown").GetComponent<Dropdown>();
			this.error = GameObject.Find("Error").GetComponent<Text>();
			this.selectedGenerator = GameObject.Find("SelectedGenerator").GetComponent<Text>();
			this.zrusitOtazkyButton = GameObject.Find("ZrusitOtazkyButton").GetComponent<Button>();

			this.zmenaOtazokButtonObject = GameObject.Find("ZmenaOtazokButton");
			this.zmenaOtazokInputObject = GameObject.Find("ZmenaOtazokInput");
			this.generatorDropdownObject = GameObject.Find("GeneratorDropdown");
			this.errorObject = GameObject.Find("Error");
			this.zrusitOtazkyButtonObject = GameObject.Find("ZrusitOtazkyButton");

			this.zmenaOtazokInputObject.SetActive(false);
			this.generatorDropdownObject.SetActive(false);
			this.errorObject.SetActive(false);
			this.zrusitOtazkyButtonObject.SetActive(false);

			this.zmenaOtazokButton.onClick.AddListener(ZmenaOtazokButtonOnClick);
			this.zrusitOtazkyButton.onClick.AddListener(ZrusitOtazkyButtonOnClick);

			PlayerManager.idGenerator = 1; // Default hodnota pre nas generator(otazky)

			StartCoroutine(this.getPlayerStats());
			StartCoroutine(this.setIdGenerator(true));
    }

		void ZrusitOtazkyButtonOnClick() {
			PlayerManager.idGenerator = 1;
			this.zmenaOtazokButtonStav = 1;
			this.zmenaOtazokInputObject.SetActive(false);
			this.generatorDropdownObject.SetActive(false);
			this.errorObject.SetActive(false);
			this.zrusitOtazkyButtonObject.SetActive(false);
			this.zmenaOtazokButtonObject.GetComponentInChildren<Text>().text = "Zmeniť otázky v hre";
			this.selectedGenerator.text = "naše otázky";
		}

		void ZmenaOtazokButtonOnClick() {
			if (this.zmenaOtazokButtonStav == 1) {
				this.zmenaOtazokInputObject.SetActive(true);
				this.zmenaOtazokButtonObject.GetComponentInChildren<Text>().text = "Použiť kód";
				this.zmenaOtazokButtonStav = 2;
			} else if(this.zmenaOtazokButtonStav == 2) {
				StartCoroutine(this.getGenerators());
			} else if(this.zmenaOtazokButtonStav == 3) {
				StartCoroutine(this.setIdGenerator());
			}
		}

		public IEnumerator getGenerators() {
			using (UnityWebRequest www = UnityWebRequest.Get(
				"https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=get_generators&uid=" + zmenaOtazokInput.text 
			)) {
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log("Databazovy error");
				} else {
					generatorDropdown.ClearOptions();
					Generators response = JsonUtility.FromJson<Generators>(www.downloadHandler.text);

					this.idRoom = response.idRoom;

					if (response.status == "success") {
						this.generatorDropdownObject.SetActive(true);
						this.zmenaOtazokInputObject.SetActive(false);
						this.errorObject.SetActive(false);
						this.zmenaOtazokButtonObject.GetComponentInChildren<Text>().text = "Použiť vybrané otázky";

						this.zmenaOtazokButtonStav = 3;

						List<string> dropdownOptions = new List<string>();
						
						foreach (Generator generator in response.data) {
							dropdownOptions.Add(generator.name);
						}

						generatorDropdown.AddOptions(dropdownOptions);
					} else if (response.status == "empty") {
						this.error.text = "Nie sú dostupné žiadne úlohy pre tento kód";
						this.errorObject.SetActive(true);
						this.zmenaOtazokButtonStav = 2;
						this.error.text = "Zadaný kód nie je platný";
					} else if (response.status == "error") {
						this.errorObject.SetActive(true);
						this.zmenaOtazokButtonStav = 2;
					}
				}

			}
		}

		public IEnumerator setIdGenerator(bool init = false) {
			var dropDownName = "";
			if (init) dropDownName = "Matematika";
			else dropDownName = this.generatorDropdown.options[this.generatorDropdown.value].text;

			using (UnityWebRequest www = UnityWebRequest.Get(
				"https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=get_generator_id&generatorName=" 
				+ dropDownName
				+ "&playerName=" + NameMenuController.playerNickname
				+ "&idRoom=" + this.idRoom
			)) {
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log("Databazovy error");
				} else {
					Debug.Log(www.downloadHandler.text);
					PlayerData response = JsonUtility.FromJson<PlayerData>(www.downloadHandler.text);

					PlayerManager.idGenerator = response.idGenerator;
					PlayerManager.idPlayer = response.idPlayer;
					PlayerManager.idPlayerGenerator = response.idPlayerGenerator;
					PlayerManager.lastPositionX = response.lastPositionX;
					PlayerManager.lastPositionY = response.lastPositionY;
					PlayerManager.lastPositionZ = response.lastPositionZ;

					if (!init) {
						this.zrusitOtazkyButtonObject.SetActive(true);
						this.selectedGenerator.text = dropDownName;
					}
				}

			}
		}

		public IEnumerator getPlayerStats() {
			// Player stats init
			this.playerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
			this.deaths = GameObject.Find("Deaths").GetComponent<Text>();
			this.uncorrectAnswers = GameObject.Find("UncorrectAnswers").GetComponent<Text>();
			this.correctAnswers = GameObject.Find("CorrectAnswers").GetComponent<Text>();

			using (UnityWebRequest www = UnityWebRequest.Get(
				"https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=get_player_stats&playerNickname=" + NameMenuController.playerNickname
			)) {
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log("Databazovy error");
				} else {
					Debug.Log(www.downloadHandler.text);
					PlayerStats response = JsonUtility.FromJson<PlayerStats>(www.downloadHandler.text);

					PlayerManager.idPlayer = response.id;
					//PlayerManager.idPlayerGenerator = response.idPlayerGenerator;

					this.playerScore.text = response.score.ToString();
					this.deaths.text = response.deaths.ToString();
					this.uncorrectAnswers.text = response.correct_answers.ToString();
					this.correctAnswers.text = response.uncorrect_answers.ToString();

				}

			}
		}

    public void PlayGame()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;

            yield return null;
        }   
    }

    public void LoadGame()
    {

    }

    public void Settings()
    {

    }

    public void Score()
    {

    }

    public void AddQuests()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }




}
