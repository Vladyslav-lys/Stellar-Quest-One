using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] PopUps;
    private int _popUpIndex = 0;
    public GameObject LevelName;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {   
        Time.timeScale = 0;
        for (int i = 0; i < PopUps.Length; i++)
        {
            if (i == _popUpIndex)
            {
                PopUps[i].SetActive(true);
            }
            else
            {
                PopUps[i].SetActive(false);
            }
        }
    }

    public void Next()
    {
        _popUpIndex++;
        if (_popUpIndex == PopUps.Length)
        {
            Destroy(gameObject);
            PlayerPrefs.SetInt("Tutorial", 1);
            Time.timeScale = 1;  
            LevelName.SetActive(true);
        }
    }
}
