using UnityEngine;

public class AudioSourceMenu : MonoBehaviour
{
    private static AudioSourceMenu instance = null;
    public static AudioSourceMenu Instance {
        get { return instance; }
    }
    
    void Awake()
    {
        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            Destroy();

            if (Instance != null)
            {
                FindObjectOfType<AudioSourceMenu>().Destroy();
            }
            return;
        }
        
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            
        }
       
        DontDestroyOnLoad(this.gameObject);
    }

    public void Destroy()
    {        
        Destroy(this.gameObject);
    }
}
