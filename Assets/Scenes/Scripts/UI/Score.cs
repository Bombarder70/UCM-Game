using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int score = 10;
    public Text text;
  
    void Update ()
    {
        text.text = score.ToString() + "x";
    }


}
