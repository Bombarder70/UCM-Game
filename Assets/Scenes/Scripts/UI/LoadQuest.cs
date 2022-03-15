using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadQuest : MonoBehaviour
{
    public Text otazkaText;
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
      public bool zobrazena;
      public bool odpoved;
    }

    void Start() {
      Quests questsInJson = JsonUtility.FromJson<Quests>(jsonFile.text);

      foreach (Quest quest in questsInJson.quests) {
        if (quest.zobrazena == false) {
          otazkaText.text = quest.otazka;
          break;
        }
      }
    }
}
