using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class Score : MonoBehaviour
{

	public static int score = 0;
	public Text text;

	public IEnumerator getPlayerScore() {
		WWWForm form = new WWWForm();

		form.AddField("playerNickname", NameMenuController.playerNickname);

		using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/holes/pirate-game/web/index.php?action=get_score", form)) {
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Score.score = 50;
			} else {
				Score.score = int.Parse(www.downloadHandler.text);
			}
				
		}
	}

	void Start() {
		PlayerManager.nickname = NameMenuController.playerNickname;
		StartCoroutine(this.getPlayerScore());
	}

	void Update () {
		text.text = score.ToString() + "x";
	}

	public string FetchMacId() {
		string macAddresses = "";

		foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
			if (nic.OperationalStatus == OperationalStatus.Up) {
				return nic.GetPhysicalAddress().ToString();
			}
		}

		return macAddresses;
	}

}
