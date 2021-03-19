using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add2Hero : MonoBehaviour
{
    public string HeroName;
    public string HeroName_2;
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
        PlayerPrefs.SetInt(HeroName_2, 0);
    }

    public void DestroyNotification()
    {
        Destroy(Notification);
        Destroy(gameObject);
    }
}
