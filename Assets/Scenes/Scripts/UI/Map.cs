using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    
    public GameObject mapa;
    private bool isActive = false;
    private GameObject head;
    public GameObject head1;
    public GameObject head2;
    public GameObject head3;
    public GameObject head4;
    public GameObject head5;
    List<GameObject> Heads = new List<GameObject>();
    public Sprite tick;
    public Sprite cross;

    Vector2 mapVector = new Vector2 (70,70);

    void Start()
    {
        Heads.Add(head1);
        Heads.Add(head2);
        Heads.Add(head3);
        Heads.Add(head4);
        Heads.Add(head5);
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

    public void ChangeCross (int questionID, bool answer)
    {
        StartCoroutine(Change(questionID,answer));
    }
    public IEnumerator Change (int questionID, bool answer)
    {
        foreach (var X in Heads)
        {
            if (X.GetComponent<Cross>().id == questionID) head = X;
        }
        
        yield return new WaitForSeconds(1.7f);
        head.transform.LeanScale(new Vector2(0.28f,0.28f), 1f);
//       cross.transform.LeanScale(new Vector2(0.8f,0.8f), 1.2f);
//        cross.transform.LeanMoveLocal(new Vector2 (-0.1f, -2.5f), 1).setEaseOutQuart();
        if (answer)
        {
            head.GetComponent<Image>().color = Color.green;
            head.GetComponent<Image>().sprite = tick;
        }
        else
        {
            head.GetComponent<Image>().color = Color.red;
            head.GetComponent<Image>().sprite = cross;
        }
//        cross.transform.LeanMoveLocal(new Vector2 (-0.1f, -2.94f), 1).setEaseOutQuart();
         yield return new WaitForSeconds(1.2f);
         Open();
    }


    

}