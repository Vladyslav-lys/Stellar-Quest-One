using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeroes : MonoBehaviour
{
    public string HeroName;
    public GameObject Notification;
    
    void Start()
    {
        if (PlayerPrefs.HasKey(HeroName))
        {
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;
        
        Notification.SetActive(true);
        PlayerPrefs.SetInt(HeroName, 0);
    }

    public void DestroyNotification()
    {
        Destroy(Notification);
        Destroy(gameObject);
    }
}
