using UnityEngine;

public class LevelAudioSource : MonoBehaviour
{
    private AudioSource Sound;
    
    void Awake()
    {
        if(AudioSourceMenu.Instance != null)
            AudioSourceMenu.Instance.Destroy();
  
        Sound = GetComponent<AudioSource>();
        Sound.Play();
        if (PlayerPrefs.GetInt("Audio") == 0)
            Stop();

    }

    public void Stop()
    {
        Sound.Stop();
    }
}
