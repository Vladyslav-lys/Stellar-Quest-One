using UnityEngine;

public class FirstStart : MonoBehaviour
{
    public GameObject FirstStartLanguage;
    public GameObject Main;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey ("Language")) 
        {
            FirstStartLanguage.SetActive(true);
            Main.SetActive(false);
        } 
    }
    
    void Start()
    {
        Input.backButtonLeavesApp = true;
        
        if (PlayerPrefs.GetInt("MaxEnergy") > 10)
        {
            PlayerPrefs.SetInt ("Energy", 10);
            PlayerPrefs.SetInt ("MaxEnergy", 10);
        }
        if (!PlayerPrefs.HasKey ("MaxEnergy"))
        {
            PlayerPrefs.SetInt ("MaxEnergy", 10);
        }
        if (!PlayerPrefs.HasKey ("Audio"))
        {
            PlayerPrefs.SetInt ("Audio", 1);
        }
        if (!PlayerPrefs.HasKey ("Energy")) 
        {
            PlayerPrefs.SetInt ("Energy", 10);
        }
        if (!PlayerPrefs.HasKey ("Skin")) 
        {
            PlayerPrefs.SetString("Skin", "Char_1");
        } 
        if (!PlayerPrefs.HasKey ("Bullet")) 
        {
            PlayerPrefs.SetString("Bullet", "Bullet_1");
        }
        if (!PlayerPrefs.HasKey ("Coins")) 
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        if (!PlayerPrefs.HasKey ("Char_1")) 
        {
            PlayerPrefs.SetInt("Char_1", 1);
        }
        if (!PlayerPrefs.HasKey ("Char_2")) 
        {
            PlayerPrefs.SetInt("Char_2", 0);
        } 
        if (!PlayerPrefs.HasKey ("Char_3")) 
        {
            PlayerPrefs.SetInt("Char_3", 0);
        } 
        if (!PlayerPrefs.HasKey ("Char_4")) 
        {
            PlayerPrefs.SetInt("Char_4", 0);
        }  
        if (!PlayerPrefs.HasKey ("Bullet_1")) 
        {
            PlayerPrefs.SetInt("Bullet_1", 1);
        }
        if (!PlayerPrefs.HasKey ("Bullet_2")) 
        {
            PlayerPrefs.SetInt("Bullet_2", 0);
        } 
        if (!PlayerPrefs.HasKey ("Bullet_3")) 
        {
            PlayerPrefs.SetInt("Bullet_3", 0);
        } 
        if (!PlayerPrefs.HasKey ("Purple_1")) 
        {
            PlayerPrefs.SetInt("Purple_1", 0);
        }
    }
}