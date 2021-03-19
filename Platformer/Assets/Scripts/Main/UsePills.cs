using UnityEngine;

public class UsePills : MonoBehaviour
{
    public void UseSlowPill()
    {
        if (PlayerPrefs.GetInt("TimePill") <= 0)
            return;
        
        PlayerPrefs.SetInt("TimePill", PlayerPrefs.GetInt("TimePill") - 1);
        Time.timeScale = 0.5f;
        FindObjectOfType<Player>().EndSlow();
        FindObjectOfType<PauseScript>().resumeButton();
    }
    
    public void UseHealthPill()
    {
        if (PlayerPrefs.GetInt("HealthPill") <= 0)
            return;
        
        var player = FindObjectOfType<Player>();
        if (player.Health != player.StartHealth)
        {
            PlayerPrefs.SetInt("HealthPill", PlayerPrefs.GetInt("HealthPill") - 1);

            player.Health += 50;
            if (player.Health > player.StartHealth)
            {
                player.Health = player.StartHealth;
            }
            FindObjectOfType<PauseScript>().resumeButton();
        }
    }
    
    public void UseBigHealthPill()
    {
        if (PlayerPrefs.GetInt("BigHealthPill") <= 0)
            return;
        
        var player = FindObjectOfType<Player>();
        if (player.Health != player.StartHealth)
        {
            PlayerPrefs.SetInt("BigHealthPill", PlayerPrefs.GetInt("BigHealthPill") - 1);

            player.Health += 75;
            if (player.Health > player.StartHealth)
            {
                player.Health = player.StartHealth;
            }
            FindObjectOfType<PauseScript>().resumeButton();
        }
    }
    
}
