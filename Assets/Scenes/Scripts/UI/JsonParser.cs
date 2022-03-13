using UnityEngine;
  
public class JsonParser : MonoBehaviour {
  public TextAsset jsonFile;

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
  }

  
  void Start() {
    Quests questsInJson = JsonUtility.FromJson<Quests>(jsonFile.text);

    foreach (Quest quest in questsInJson.quests) {
        Debug.Log("Found quest: " + quest.id + " " + quest.otazka);
    }
  }
}
