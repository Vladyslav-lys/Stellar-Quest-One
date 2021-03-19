using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject Star_1;
    public GameObject Star_2;
    public GameObject Complete_2;
    public GameObject Star_3;
    public GameObject Complete_3;
    public TextMeshProUGUI TimeTMP;
    public TextMeshProUGUI CoinsTMP;
    public GameObject UICanvas;
    public GameObject FinishCanvas;
    public string NextLevelName;
    public AudioClip LevelEnd;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;

        Time.timeScale = 0;
        CoinsTMP.text  = LevelManager.Instance.Coins + " / " + LevelManager.Instance.MaxCoins;
        TimeTMP.text  =  Mathf.RoundToInt(LevelManager.Instance.PlayLevelTime) + " / " +  Mathf.RoundToInt(LevelManager.Instance.StartLevelTime);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + LevelManager.Instance.Coins);
        PlayerPrefs.SetInt("CollectCoins", PlayerPrefs.GetInt("CollectCoins") + LevelManager.Instance.Coins);
        Finish();  
    }

    private void Finish()
    {
        var stars = 1;
        
        if(LevelEnd != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (LevelEnd, transform.position);

        if (LevelManager.Instance.PlayLevelTime < LevelManager.Instance.StartLevelTime)
            stars++;
        
        if (LevelManager.Instance.Coins == LevelManager.Instance.MaxCoins)
            stars++;
        
        Star_2.SetActive(stars > 1);
        Star_3.SetActive(stars > 2);
        Complete_2.SetActive(LevelManager.Instance.Coins == LevelManager.Instance.MaxCoins);
        Complete_3.SetActive(LevelManager.Instance.PlayLevelTime < LevelManager.Instance.StartLevelTime);

        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) < stars)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, stars);
        }

        UICanvas.SetActive(false);
        FinishCanvas.SetActive(true);

        if (!PlayerPrefs.HasKey(NextLevelName))
        {
            PlayerPrefs.SetInt(NextLevelName, 0);
        }

        if (!PlayerPrefs.HasKey("Lava_1") && PlayerPrefs.GetInt("Forest_10") > 0)
        {
            PlayerPrefs.SetInt("Lava_1", 0);
        }
    }
}
