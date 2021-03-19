using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public string LevelName;
    public AudioClip ButtonSelect;
    
    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene (LevelName);
        if(ButtonSelect != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
    }
}
