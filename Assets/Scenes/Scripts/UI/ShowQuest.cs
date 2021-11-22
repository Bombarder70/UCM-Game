using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQuest : MonoBehaviour
{
    public GameObject quest;

    void OnTriggerEnter ()
    {
        quest.SetActive (true);
    }
}
