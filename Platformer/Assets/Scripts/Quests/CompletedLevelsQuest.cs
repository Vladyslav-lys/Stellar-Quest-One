using UnityEngine;

public class CompletedLevelsQuest : AbstractQuest
{
    private string _currentLevelName;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("CompleteLevel"))
        {
            PlayerPrefs.SetString("CompleteLevel", "Purple_3");
            PlayerPrefs.SetInt("CompleteLevelCoins", 25);
            SetText();
        }
        else
        {
            SetText();
        } 
    }

    private void SetQuest()
    {
        if (PlayerPrefs.GetInt("CompleteLevelCoins") >= 100)
        {
            PlayerPrefs.SetInt("CompleteLevelCoins", PlayerPrefs.GetInt("CompleteLevelCoins") + 50);
        }
        else
        {
            PlayerPrefs.SetInt("CompleteLevelCoins", PlayerPrefs.GetInt("CompleteLevelCoins") * 2);
        }
        switch (PlayerPrefs.GetString("CompleteLevel"))
        {
            case "Purple_3":
                PlayerPrefs.SetString("CompleteLevel", "Purple_7");
                break;
            case "Purple_7":
                PlayerPrefs.SetString("CompleteLevel", "Purple_10");
                break;
            case "Purple_10":
                PlayerPrefs.SetString("CompleteLevel", "Forest_5");
                break;
            case "Forest_5":
                PlayerPrefs.SetString("CompleteLevel", "Forest_10");
                break;
            case "Forest_10":
                PlayerPrefs.SetString("CompleteLevel", "Lava_5");
                break;
            case "Lava_5":
                PlayerPrefs.SetString("CompleteLevel", "Lava_10");
                break;
            case "Lava_10":
                PlayerPrefs.SetString("CompleteLevel", "Test");
                break;
        }

        SetText();
    }
    
    private void SetText()
    {
        var done = PlayerPrefs.GetInt(PlayerPrefs.GetString("CompleteLevel")) > 0;
        Done.SetActive(done);
        NotDone.SetActive(!done);

        SetName();
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            QuestDescriptionTMP.text = RussianDescription + " " + _currentLevelName;
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            QuestDescriptionTMP.text = EnglishDescription + " " + _currentLevelName;
        }
          
        QuestCoinsTMP.text = "+" + PlayerPrefs.GetInt("CompleteLevelCoins");
    }
    
    private void SetName()
    {
        switch (PlayerPrefs.GetString("CompleteLevel"))
        {
            case "Purple_3":
                _currentLevelName = "Purble planet 3";
                break;
            case "Purple_7":
                _currentLevelName = "Purble planet 7";
                break;
            case "Purple_10":
                _currentLevelName = "Purble planet 10";
                break;
            case "Forest_5":
                _currentLevelName = "Fortrest 5";
                break;
            case "Forest_10":
                _currentLevelName = "Fortrest 10";
                break;
            case "Lava_5":
                _currentLevelName = "Lava 5";
                break;
            case "Lava_10":
                _currentLevelName = "Lava 10";
                break;
            case "Test":
                if (PlayerPrefs.GetString("Language") == "Russian")
                {
                    _currentLevelName = "Все уровни пройдены";
                } 
                if (PlayerPrefs.GetString("Language") == "English")
                {
                    _currentLevelName = "All levels completed";
                }
                break;
        }
    }
    
    public void GetCoins()
    {
        if (PlayerPrefs.GetString("CompleteLevel") == "Purple_3")
        {
            PlayerPrefs.SetInt("ShopTutorial", 2);
        }
        
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("CompleteLevelCoins"));
        SetQuest();
    }
}
