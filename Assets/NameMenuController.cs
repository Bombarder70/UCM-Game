using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class NameMenuController : MonoBehaviour {
	public Text playerNicknameText;
	public Text playAsText;
  public InputField playerNicknameInput;

	public Button playButton;
  public Button changeNicknameButton;

	public GameObject playerNicknameTextObject;
	public GameObject playAsTextObject;

	public GameObject playerNicknameInputObject;

	public GameObject changeNicknameButtonObject;
	public GameObject playButtonObject;

	public GameObject playerNicknameIsEmpty;
	public Text playerNicknameIsEmptyText;

	public static string playerNickname = "";

	public bool startGame = true;

	[System.Serializable]
	public class SetNicknameResponse {
		public string status;
		public string message;
	}

	void Start() {
		this.playerNicknameTextObject.SetActive(false);
		this.playAsTextObject.SetActive(false);

		this.playerNicknameInputObject.SetActive(false);
		this.changeNicknameButtonObject.SetActive(false);

		this.changeNicknameButton.onClick.AddListener(ChangeNicknameOnClick);
		this.playButton.onClick.AddListener(PlayButtonOnClick);

		this.playerNicknameIsEmpty.SetActive(false);

		StartCoroutine(this.getSavedPlayer());
	}

	// Update is called once per frame
	void Update() {
			
	}

	private string FetchMacId() {
		string macAddresses = "";

		foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
			if (nic.OperationalStatus == OperationalStatus.Up) {
				return nic.GetPhysicalAddress().ToString();
			}
		}

		return macAddresses;
	}

	public IEnumerator getSavedPlayer() {
		string macAddress = this.FetchMacId();

		using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/holes/pirate-game/web/index.php?action=get_saved_player&uid=" + macAddress)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log("Nenacitalo zo servera");
			} else {
				if (www.downloadHandler.text != "") {
					this.playerNicknameTextObject.SetActive(true);
					this.playAsTextObject.SetActive(true);
					this.playerNicknameInputObject.SetActive(false);

					this.changeNicknameButtonObject.SetActive(true);

					NameMenuController.playerNickname = www.downloadHandler.text;
					this.playerNicknameText.text = www.downloadHandler.text;
					this.playerNicknameInput.text = www.downloadHandler.text;
				} else {
					this.playerNicknameInputObject.SetActive(true);
				}
			}

		}
	}

	public IEnumerator setPlayerNickname(string finalNameToPlay) {
		string macAddress = this.FetchMacId();

		WWWForm form = new WWWForm();

		form.AddField("playerNickname", finalNameToPlay);
		form.AddField("uid", macAddress);
		

		using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/holes/pirate-game/web/index.php?action=set_nickname", form)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log("Chyba servera");
			} else {
				SetNicknameResponse res = JsonUtility.FromJson<SetNicknameResponse>(www.downloadHandler.text);
			
				if (res.status == "success") {
					NameMenuController.playerNickname = finalNameToPlay;
				} else if(res.status == "fail") {
					this.playerNicknameIsEmptyText.text = "Tento nickname už existuje";
					this.playerNicknameIsEmpty.SetActive(true);
				}

				if (this.startGame != false) {
					Application.LoadLevel("MainMenu");
				}
			}

		}
	}

	void PlayButtonOnClick() {
		this.playerNicknameIsEmpty.SetActive(false);
		this.startGame = true;
		this.playerNicknameIsEmptyText.text = "Nickname nemôže byť prázdny";
		string finalNameToPlay = "";

		if (this.playerNicknameInput.text != NameMenuController.playerNickname) {
			if (this.playerNicknameInput.text == "") {
				this.playerNicknameIsEmpty.SetActive(true);
				this.startGame = false;
			} else {
				StartCoroutine(this.setPlayerNickname(this.playerNicknameInput.text));
			}
		} else {
			StartCoroutine(this.setPlayerNickname(NameMenuController.playerNickname));
		}
		
	}

	void ChangeNicknameOnClick() {
		this.playerNicknameTextObject.SetActive(false);
		this.playAsTextObject.SetActive(false);
	  this.playerNicknameInputObject.SetActive(true);
		this.changeNicknameButtonObject.SetActive(false);
	}
}
