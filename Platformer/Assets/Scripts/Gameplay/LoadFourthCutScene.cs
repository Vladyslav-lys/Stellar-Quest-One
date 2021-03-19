using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFourthCutScene : MonoBehaviour
{
    public AudioClip ButtonSelect;
    
    public void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene("FourthCutSceneRus");
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene("FourthCutScene");
        if(ButtonSelect != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
    }
}
