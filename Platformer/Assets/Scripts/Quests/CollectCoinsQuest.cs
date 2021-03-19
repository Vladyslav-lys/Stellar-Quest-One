using UnityEngine;

public class CollectCoinsQuest : AbstractQuest
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentCollectCoins"))
        {
            PlayerPrefs.SetInt("CurrentCollectCoins", 25);
            PlayerPrefs.SetInt("CurrentGetCoins", 1);
            PlayerPrefs.SetInt("CollectCoins", 0);
            SetText();
        }
        else
        {
            SetText();
        } 
    }

    private void SetQuest()
    {
        PlayerPrefs.SetInt("CurrentCollectCoins", PlayerPrefs.GetInt("CurrentCollectCoins") * 2);
        PlayerPrefs.SetInt("CurrentGetCoins", PlayerPrefs.GetInt("CurrentGetCoins") + 1);
        SetText();
    }
    
    private void SetText()
    {
        var done = PlayerPrefs.GetInt("CollectCoins") >= PlayerPrefs.GetInt("CurrentCollectCoins");
        Done.SetActive(done);
        NotDone.SetActive(!done);

        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            QuestDescriptionTMP.text = RussianDescription + " " + PlayerPrefs.GetInt("CurrentCollectCoins")
                                       + "  монет " + "(" +PlayerPrefs.GetInt("CollectCoins") + "/" + PlayerPrefs.GetInt("CurrentCollectCoins") + ")";
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            QuestDescriptionTMP.text = EnglishDescription + " " + PlayerPrefs.GetInt("CurrentCollectCoins")
                                       + "  ExyCoins " + "(" +PlayerPrefs.GetInt("CollectCoins") + "/" + PlayerPrefs.GetInt("CurrentCollectCoins") + ")";
        }
          
        QuestCoinsTMP.text = "+" + PlayerPrefs.GetInt("CurrentGetCoins") * 10;
    }
    
    public void GetCoins()
    {
        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("CurrentGetCoins") * 10));
        PlayerPrefs.SetInt("CollectCoins", 0);
        SetQuest();
    }
}
