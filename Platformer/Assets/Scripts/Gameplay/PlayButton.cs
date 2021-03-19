using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string LevelName;
    public AudioClip ButtonSelect;

    public void Select()
    {
        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            if (PlayerPrefs.GetString("Language") == "Russian")
                SceneManager.LoadScene("FirstCutScenesRus");
            if (PlayerPrefs.GetString("Language") == "English")
                SceneManager.LoadScene("FirstCutScenes");

            
            if(PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint(ButtonSelect, transform.position);
                
            PlayerPrefs.SetInt("FirstPlay", 1);
            return;
        }
        SceneManager.LoadScene(LevelName);
        if(PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint(ButtonSelect, transform.position);
    }
}
