using UnityEngine;

public class SellCoinsQuest : AbstractQuest
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentSellCoins"))
        {
            PlayerPrefs.SetInt("CurrentSellCoins", 1);
            PlayerPrefs.SetInt("SellCoins", 0);
            SetText();
        }
        else
        {
            SetText();
        } 
    }

    private void SetQuest()
    {
        PlayerPrefs.SetInt("CurrentSellCoins", PlayerPrefs.GetInt("CurrentSellCoins") + 2);
        SetText();
    }
    
    private void SetText()
    {
        var done = PlayerPrefs.GetInt("SellCoins") >= PlayerPrefs.GetInt("CurrentSellCoins") * 50;
        Done.SetActive(done);
        NotDone.SetActive(!done);

        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            QuestDescriptionTMP.text = RussianDescription + " " + PlayerPrefs.GetInt("CurrentSellCoins") * 50
                                       + "  монет " + "(" +PlayerPrefs.GetInt("SellCoins") + "/" + PlayerPrefs.GetInt("CurrentSellCoins") * 50 + ")";
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            QuestDescriptionTMP.text = EnglishDescription + " " + PlayerPrefs.GetInt("CurrentSellCoins") * 50
                                       + " ExyCoins" + "(" +PlayerPrefs.GetInt("SellCoins") + "/" + PlayerPrefs.GetInt("CurrentSellCoins") * 50 + ")";
        }
          
        QuestCoinsTMP.text = "+" + PlayerPrefs.GetInt("CurrentSellCoins") * 10;
    }
    
    public void GetCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("CurrentSellCoins") * 10);
        PlayerPrefs.SetInt("SellCoins", 0);
        SetQuest();
    }
}
