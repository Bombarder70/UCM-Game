using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WelcomeUI : MonoBehaviour
{
    
    [SerializeField]
    GameObject papier;
    Vector3 papierVector = new Vector3 (500,500,200);
    [SerializeField]
    Text pozdrav;
    [SerializeField]
    Text controls;
    [SerializeField]
    Text controlsText;
    Color newColor = new Color(0.3f, 0.4f, 0.6f, 0.0f);

    void Start()
    {
        controls.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 0.01f, false);
        controlsText.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 0.01f, false);
        papier.transform.localScale = Vector2.zero;
        papier.SetActive(true);
        papier.transform.LeanScale(papierVector, 1.4f);
        Debug.Log("qqqqqqq");
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(7);
        pozdrav.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 2.05f, false);
        yield return new WaitForSeconds(2.05f);
        controls.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(1.0f, 2.05f, false);
        controlsText.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(1.0f, 2.05f, false);
        yield return new WaitForSeconds(5);
        papier.transform.LeanScale(Vector2.zero, 1.4f).setEaseInBack();
        gameObject.SetActive(false);
       // pozdrav.transform.LeanMoveLocalX(-Screen.height, 6.5f).setEaseInBack();
    }
}
