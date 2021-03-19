using UnityEngine;
using UnityEngine.SceneManagement;

public class GetCoins : MonoBehaviour
{
    public void GetPlayerCoins(int coin)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coin);
    }

    public void ResetHero()
    {
        PlayerPrefs.SetString("Skin", "Char_1");
        PlayerPrefs.SetString("Bullet", "Bullet_1");
        PlayerPrefs.SetInt("Char_2", 0);
        PlayerPrefs.SetInt("Char_3", 0);
        PlayerPrefs.SetInt("Char_4", 0);
        PlayerPrefs.SetInt("Bullet_2", 0);
        PlayerPrefs.SetInt("Bullet_3", 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetLevels()
    {
        PlayerPrefs.SetInt("Purple_1", 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.DeleteKey("Purple_2");
        PlayerPrefs.DeleteKey("Purple_3");
        PlayerPrefs.DeleteKey("Purple_3");
        PlayerPrefs.DeleteKey("Purple_4");
        PlayerPrefs.DeleteKey("Purple_5");
        PlayerPrefs.DeleteKey("Purple_6");
        PlayerPrefs.DeleteKey("Purple_7");
        PlayerPrefs.DeleteKey("Purple_7");
        PlayerPrefs.DeleteKey("Purple_8");
        PlayerPrefs.DeleteKey("Purple_9");
        PlayerPrefs.DeleteKey("Purple_10");
        PlayerPrefs.DeleteKey("Forest_1");
        PlayerPrefs.DeleteKey("Forest_2");
        PlayerPrefs.DeleteKey("Forest_3");
        PlayerPrefs.DeleteKey("Forest_4");
        PlayerPrefs.DeleteKey("Forest_5");
        SceneManager.LoadScene("MainMenu");
    }

    public void UnlockLevels()
    {
        PlayerPrefs.SetInt("Purple_2", 0);
        PlayerPrefs.SetInt("Purple_2", 0);
        PlayerPrefs.SetInt("Purple_3", 0);
        PlayerPrefs.SetInt("Purple_4", 0);
        PlayerPrefs.SetInt("Purple_5", 0);
        PlayerPrefs.SetInt("Purple_6", 0);
        PlayerPrefs.SetInt("Purple_7", 0);
        PlayerPrefs.SetInt("Purple_8", 0);
        PlayerPrefs.SetInt("Purple_9", 0);
        PlayerPrefs.SetInt("Purple_10", 0);
        PlayerPrefs.SetInt("Forest_1", 0);
        PlayerPrefs.SetInt("Forest_2", 0);
        PlayerPrefs.SetInt("Forest_3", 0);
        PlayerPrefs.SetInt("Forest_4", 0);
        PlayerPrefs.SetInt("Forest_5", 0);
        PlayerPrefs.SetInt("Forest_6", 0);
        PlayerPrefs.SetInt("Forest_7", 0);
        PlayerPrefs.SetInt("Forest_8", 0);
        PlayerPrefs.SetInt("Forest_9", 0);
        PlayerPrefs.SetInt("Forest_10", 0);
        PlayerPrefs.SetInt("Lava_1", 0);
        PlayerPrefs.SetInt("Lava_2", 0);
        PlayerPrefs.SetInt("Lava_3", 0);
        PlayerPrefs.SetInt("Lava_4", 0);
        PlayerPrefs.SetInt("Lava_5", 0);
        PlayerPrefs.SetInt("Lava_6", 0);
        PlayerPrefs.SetInt("Lava_7", 0);
        PlayerPrefs.SetInt("Lava_8", 0);
        PlayerPrefs.SetInt("Lava_9", 0);
        PlayerPrefs.SetInt("Lava_10", 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Skin", "Char_1");
        PlayerPrefs.SetString("Bullet", "Bullet_1");
        SceneManager.LoadScene("MainMenu");
    }

    public void Tutorial()
    {
        PlayerPrefs.DeleteKey("Tutorial");
        SceneManager.LoadScene("Purple_1");
    }

    public void FirstCutScene()
    {
        SceneManager.LoadScene("FirstCutScenes");
    }
    
    public void SecondCutScene()
    {
        SceneManager.LoadScene("SecondCutScene");
    }
}
