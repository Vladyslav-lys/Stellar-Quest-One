using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltips : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public float HideTime;
    public GameObject Tooltip;
    public string RussianSentences;
    public string EnglishSentences;

    private int _index;

    public void Update()
    {
        Tooltip.transform.position = new Vector3(FindObjectOfType<Player>().transform.position.x + 4.5f, FindObjectOfType<Player>().transform.position.y + 5.5f, 0);
    }
    
    IEnumerator Type()
    {
        if (PlayerPrefs.GetString("Skin") == "Char_3")
        {
            if (PlayerPrefs.GetString("Language") == "Russian")
            {    
                string[] theo = new string[]
                {
                    "Вуф",
                    "Гав",
                    "Вуууууф",
                }; 
                TextDisplay.text += theo[Random.Range(0, theo.Length)];    
            } 
            if (PlayerPrefs.GetString("Language") == "English")
            {
                string[] theo = new string[]
                {
                    "Woooooof",
                    "Woof",
                    "Bork",
                };
                TextDisplay.text += theo[Random.Range(0, theo.Length)];    
            }
        }
        else
        {
            if (PlayerPrefs.GetString("Language") == "Russian")
            {    
                TextDisplay.text += RussianSentences;    
            } 
            if (PlayerPrefs.GetString("Language") == "English")
            {
                TextDisplay.text += EnglishSentences;
            }
        }       
       
        yield return new WaitForSecondsRealtime(HideTime);
        Tooltip.SetActive(false);
        TextDisplay.text = "";
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player" && !Tooltip.activeSelf)
        {
            Tooltip.SetActive(true);
            StartCoroutine(Type());   
        }  
    }
}
