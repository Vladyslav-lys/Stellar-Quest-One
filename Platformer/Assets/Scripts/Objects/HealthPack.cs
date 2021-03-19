using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IPlayerRespawnListener
{
    public int Health;
    public AudioClip HealthSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
            return;

        var player = FindObjectOfType<Player>();
        if (player.Health != player.StartHealth)
        {
            if(HealthSound != null && PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (HealthSound, transform.position);

            player.Health += Health;
            if (player.Health > player.StartHealth)
            {
                player.Health = player.StartHealth;
            }
            gameObject.SetActive(false);
            FloatingText.Show (string.Format ("+{0} HP", Health), "HealthText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1.5f, 50));
        }
    }

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        if(gameObject.activeSelf)
            return;
        
        gameObject.SetActive(true);

    }
}