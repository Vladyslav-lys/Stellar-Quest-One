using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingCutScene : MonoBehaviour
{

    public string UnlockLevelName;
    public string RussianCutSceneName;
    public string EnglishCutSceneName;

    void Start()
    {
        gameObject.SetActive(PlayerPrefs.HasKey(UnlockLevelName));   
    }

    public void ShowCutScene()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene(RussianCutSceneName);
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene(EnglishCutSceneName);
    }
}
