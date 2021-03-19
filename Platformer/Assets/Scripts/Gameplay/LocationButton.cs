using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationButton : MonoBehaviour
{
    public string OpenLevelName;
    public string LevelName;
    public GameObject LockIcon;
    public AudioClip ButtonSelect;

    void Start()
    {
        LockIcon.SetActive(!PlayerPrefs.HasKey(OpenLevelName));
    }
    
    public void OnMouseUpAsButton()
    {
        
        SceneManager.LoadScene (LevelName);
        if(ButtonSelect != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
    }
}
