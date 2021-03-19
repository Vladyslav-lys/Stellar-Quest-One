using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSetting : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public string EnglishOff;
    public string EnglishOn; 
    public string RussianOff;
    public string RussianOn;
    
    void Start()
    {
        SetText();
    }

    public void ChangeMusic()
    {
        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            PlayerPrefs.SetInt("Audio", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 0);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void SetText()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            Text.text = PlayerPrefs.GetInt("Audio") == 0 ? RussianOff : RussianOn;
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            Text.text = PlayerPrefs.GetInt("Audio") == 0 ? EnglishOff : EnglishOn;
        }     
    }
}
