using TMPro;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public string SkinName;
    public TextMeshProUGUI BuyTMP;
    public int Price;
    public GameObject CoinsIMG;
    public string Russian;
    public string English;
    public AudioClip ButtonSelect;
    public GameObject SelectedImage;
    public GameObject BlockImage;
    public string KeyName;
    public bool IsConor = false;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey(KeyName))
        {
            BlockImage.SetActive(true);
        }
        else
        {
            BlockImage.SetActive(false);
        }
        
        if(IsConor)
            BlockImage.SetActive(false);

        if (PlayerPrefs.GetInt(SkinName) == 0)
        {  
            SelectedImage.SetActive(false);
            CoinsIMG.SetActive(true);
            BuyTMP.text =Price + " ExyCoins";
        }
        else
        {
           ChangeText();
        }
    }

    public void ChangePlayerSkin()
    {
        if (PlayerPrefs.GetInt(SkinName) == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= Price)
            {
               PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Price);
               PlayerPrefs.SetInt("SellCoins", Price);
               PlayerPrefs.SetInt(SkinName, 1);
               ChangeText();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Audio") != 0) 
            {
                AudioSource.PlayClipAtPoint(ButtonSelect, transform.position);
            }
            
            PlayerPrefs.SetString("Skin", SkinName);
            ChangeText();
        }
    }

    public void ChangeText()
    {
        if (PlayerPrefs.GetInt(SkinName) == 1 || IsConor)
        {
            CoinsIMG.SetActive(false);

            if (PlayerPrefs.GetString("Language") == "Russian")
            {    
                BuyTMP.text = Russian;    
            } 
            if (PlayerPrefs.GetString("Language") == "English")
            {
                BuyTMP.text = English;
            }
        }

        if (PlayerPrefs.GetString("Skin") == SkinName)
        {
            SelectedImage.SetActive(true);
            return;
        }
        
        SelectedImage.SetActive(false);
        
        
    }
}
