using UnityEngine;

public class Pills : MonoBehaviour
{
    public int TimePillCoins;
    public int HealhPillCoins;
    public int BigHealthPillCoins;
    public AudioClip ButtonSelect;

    void Start()
    {
        if (!PlayerPrefs.HasKey("TimePill"))
        {
            PlayerPrefs.SetInt("TimePill", 0);
        }
        if (!PlayerPrefs.HasKey("HealthPill"))
        {
            PlayerPrefs.SetInt("HealthPill", 0);
        }
        if (!PlayerPrefs.HasKey("BigHealthPill"))
        {
            PlayerPrefs.SetInt("BigHealthPill", 0);
        }
    }

    public void BuyTimePill()
    {
        if (PlayerPrefs.GetInt("Coins") >= TimePillCoins)
        {
            if(PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - TimePillCoins);
            PlayerPrefs.SetInt("TimePill", PlayerPrefs.GetInt("TimePill") + 1);
            PlayerPrefs.SetInt("SellCoins", PlayerPrefs.GetInt("SellCoins") + TimePillCoins);
        }
    }
    
    public void BuyHealthPill()
    {
        if (PlayerPrefs.GetInt("Coins") >= HealhPillCoins)
        {
            if(PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - HealhPillCoins);
            PlayerPrefs.SetInt("HealthPill", PlayerPrefs.GetInt("HealthPill") + 1);
            PlayerPrefs.SetInt("SellCoins", PlayerPrefs.GetInt("SellCoins") + HealhPillCoins);
        }
    }
    
    public void BuyBigHealthPill()
    {
        if (PlayerPrefs.GetInt("Coins") >= BigHealthPillCoins)
        {
            if(PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - BigHealthPillCoins);
            PlayerPrefs.SetInt("BigHealthPill", PlayerPrefs.GetInt("BigHealthPill") + 1);
            PlayerPrefs.SetInt("SellCoins", PlayerPrefs.GetInt("SellCoins") + BigHealthPillCoins);
        }
    }
}