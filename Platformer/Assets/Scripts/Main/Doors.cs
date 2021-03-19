using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject Door;
    public AudioClip CrystalSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;
        
        if(CrystalSound != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (CrystalSound, transform.position);
        
        Door.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
