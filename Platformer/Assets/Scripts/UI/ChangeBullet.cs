using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeBullet : MonoBehaviour
{
    public string BulletName;
    public TextMeshProUGUI BuyTMP;
    public int Price;
    public GameObject CoinsIMG;
    public AudioClip ButtonSelect;
    public GameObject SelectedImage;
    
    void Start()
    {
        if (PlayerPrefs.GetInt(BulletName) == 0)
        {
            SelectedImage.SetActive(false);
            BuyTMP.text = Price.ToString();
        }
        else
        {
            ChangeText();
        }
    }
    
    public void ChangeBulletSkin()
    {
        if (PlayerPrefs.GetInt(BulletName) == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= Price)
            {
                if (PlayerPrefs.GetInt("ShopTutorial") == 3)
                {
                    PlayerPrefs.SetInt("ShopTutorial", 4);
                }              
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Price);
                PlayerPrefs.SetInt("SellCoins", Price);
                PlayerPrefs.SetInt(BulletName, 1);
                
                if(BulletName == "Bullet_2")
                    PlayerPrefs.SetInt("DeleteShop", 1);
                
                foreach (var Bullet in FindObjectsOfType<ChangeBullet>())
                {
                    Bullet.ChangeText();
                }
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("ShopTutorial") == 4)
            {
                PlayerPrefs.SetInt("ShopTutorial", 5);
            }

            if (PlayerPrefs.GetInt("Audio") != 0)
            {
                AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);
            }
            PlayerPrefs.SetString("Bullet", BulletName);

            foreach (var Bullet in FindObjectsOfType<ChangeBullet>())
            {
                Bullet.ChangeText();
            }
        }
    }

    private void ChangeText()
    {
        if (PlayerPrefs.GetInt(BulletName) != 1)
            return;

        CoinsIMG.SetActive(false);
        if (PlayerPrefs.GetString("Language") == "Russian")
        {    
            BuyTMP.text = "Выбрать";    
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            BuyTMP.text = "Select";
        }
        
        if (PlayerPrefs.GetString("Bullet") == BulletName)
        {
            SelectedImage.SetActive(true);
            return;
        }
        
        SelectedImage.SetActive(false);
    }
}
