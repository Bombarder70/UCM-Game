using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

   	public void QuitGame() {
			StartCoroutine(this.exitAndSave());
    }

		public IEnumerator exitAndSave() {
			WWWForm form = new WWWForm();

			form.AddField("idPlayerGenerator", PlayerManager.idPlayerGenerator);
			form.AddField("position_x", PlayerManager.instance.player.transform.position.x.ToString());
			form.AddField("position_y", PlayerManager.instance.player.transform.position.y.ToString());
			form.AddField("position_z", PlayerManager.instance.player.transform.position.z.ToString());
		
			using (UnityWebRequest www = UnityWebRequest.Post(
				"https://grid3.kaim.fpv.ucm.sk/~patrikholes/pirate-game/web/index.php?action=update_player_data", form
			)) {
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log("Databazovy error");
				} else {
					Debug.Log(www.downloadHandler.text);
				}

				Application.Quit();

			}
		}
}
