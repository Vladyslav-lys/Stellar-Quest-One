using UnityEngine;

public class HeroNotification : MonoBehaviour
{
    public GameObject Notification;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AddElen"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;
        
        Notification.SetActive(true);
    }

    public void HideNotification()
    {
        Destroy(Notification);
        Destroy(gameObject);
    }
}
