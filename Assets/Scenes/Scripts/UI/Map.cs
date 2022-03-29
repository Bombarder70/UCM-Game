using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    
    public GameObject mapa;
    private bool isActive = false;
    private GameObject cross;
    public GameObject cross1;
    public GameObject cross2;
    public GameObject cross3;
    public GameObject cross4;
    public GameObject cross5;
    List<GameObject> Crosses = new List<GameObject>();
    public Sprite tick;

    Vector2 mapVector = new Vector2 (70,70);

    void Start()
    {
        Crosses.Add(cross1);
        Crosses.Add(cross2);
        Crosses.Add(cross3);
        Crosses.Add(cross4);
        Crosses.Add(cross5);
    }
    void Update()
    {
        if (Input.GetKeyDown("m")) Open();
    }


    public void Open()
    {
        if (!isActive)
        {
            isActive = true;
            mapa.SetActive(true);
            transform.LeanScale(mapVector, 1.2f);
        }
        else
        {
            transform.LeanScale(Vector2.zero, 0.7f).setEaseInBack();
            isActive = false;
        }
    }

    public void ChangeCross (int questionID)
    {
        StartCoroutine(Change(questionID));
    }
    public IEnumerator Change (int questionID)
    {
        foreach (var X in Crosses)
        {
            if (X.GetComponent<Cross>().id == questionID) cross = X;
        }
        
        yield return new WaitForSeconds(1.7f);
        cross.transform.LeanScale(new Vector2(1.4f,1.4f), 1f);
//       cross.transform.LeanScale(new Vector2(0.8f,0.8f), 1.2f);
//        cross.transform.LeanMoveLocal(new Vector2 (-0.1f, -2.5f), 1).setEaseOutQuart();
        cross.GetComponent<Image>().color = Color.green;
        cross.GetComponent<Image>().sprite = tick;
//        cross.transform.LeanMoveLocal(new Vector2 (-0.1f, -2.94f), 1).setEaseOutQuart();
         yield return new WaitForSeconds(1.2f);
         Open();
    }
}