using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuests : AbstractQuest
{
    private string _englishEnemyName;
    private string _russianEnemyName;
    public string[] EnemyName;
    private int[] _counts = new[] {5, 10};
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentEnemy"))
        {
            SetQuest();
        }
        else
        {
            SetText();
        } 
    }

    private void SetQuest()
    {
        PlayerPrefs.SetString("CurrentEnemy", EnemyName[Random.Range(0, EnemyName.Length)]);
        PlayerPrefs.SetInt(PlayerPrefs.GetString("CurrentEnemy"), 0);
        PlayerPrefs.SetInt("CurrentCount", _counts[Random.Range(0, _counts.Length)]);
        SetText();
    }
    
    private void SetText()
    {
        var done = PlayerPrefs.GetInt(PlayerPrefs.GetString("CurrentEnemy")) >= PlayerPrefs.GetInt("CurrentCount");
        Done.SetActive(done);
        NotDone.SetActive(!done);

        SetName();
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            QuestDescriptionTMP.text = RussianDescription + " " + PlayerPrefs.GetInt("CurrentCount")
                                       + " " + _russianEnemyName + "(" + PlayerPrefs.GetInt(PlayerPrefs.GetString("CurrentEnemy")) + "/" + PlayerPrefs.GetInt("CurrentCount") + ")";
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            QuestDescriptionTMP.text = EnglishDescription + " " + PlayerPrefs.GetInt("CurrentCount")
                                       + " " + _englishEnemyName + "(" + PlayerPrefs.GetInt(PlayerPrefs.GetString("CurrentEnemy")) + "/" + PlayerPrefs.GetInt("CurrentCount") + ")";
        }
          
        QuestCoinsTMP.text = "+" + PlayerPrefs.GetInt("CurrentCount") * 3;
    }

    private void SetName()
    {
        switch (PlayerPrefs.GetString("CurrentEnemy"))
        {
            case "Worm":
                _russianEnemyName = "червей";
                _englishEnemyName = "worms";
                break;
            case "Spider":
                _russianEnemyName = "пауков";
                _englishEnemyName = "spiders";
                break;
            case "Slime_1":
                _russianEnemyName = "зеленой слизи";
                _englishEnemyName = "green slime";
                break;
            case "Slime":
                _russianEnemyName = "розовой слизи";
                _englishEnemyName = "pink slime";
                break;
            case "Insect":
                _russianEnemyName = "насекомых";
                _englishEnemyName = "insect";
                break;
        }
    }
    
    public void GetCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("CurrentCount") * 3);
        SetQuest();
    }
}
