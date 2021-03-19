using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PausePillsButton : MonoBehaviour
{
    public string ShowTextRussian;
    public string ShowTextEnglish;
    public string HideTextRussian;
    public string HideTextEnglish;
    public GameObject Pills;
    
    void Start()
    {
        Pills = GameObject.FindGameObjectWithTag("PillsCanvas");
        Pills.SetActive(PlayerPrefs.GetInt("PillsMenu") == 1);
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Pills.activeSelf
                ? HideTextRussian
                : ShowTextRussian;
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Pills.activeSelf
                ? HideTextEnglish
                : ShowTextEnglish;
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Pills.activeSelf
                ? HideTextRussian
                : ShowTextRussian;
            
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Pills.activeSelf
                ? HideTextEnglish
                : ShowTextEnglish;
        }
    }
}
