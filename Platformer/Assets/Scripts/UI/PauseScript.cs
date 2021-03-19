using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Button Pause;
    public Button Resume;
    public Button Resume_2;
    public Button GoToMenu;
    public Button Restart;
    public Button Pills;
    public GameObject UiCanvas;
    public GameObject PauseCanvas;
    public GameObject PillsGO;

    
    void Start()
    {
        PlayerPrefs.GetInt("PillsMenu");
        Pause.GetComponent<Button>();
        Pause.onClick.AddListener(pauseButton);
        Resume.GetComponent<Button>();
        Resume.onClick.AddListener(resumeButton);
        Resume_2.GetComponent<Button>();
        Resume_2.onClick.AddListener(resumeButton);
        GoToMenu.GetComponent<Button>();
        GoToMenu.onClick.AddListener(goToMenuButton);
        Restart.GetComponent<Button>();
        Restart.onClick.AddListener(restartButton);
        Pills.GetComponent<Button>();
        Pills.onClick.AddListener(pillsButton);
        UiCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void resumeButton()
    {
       UiCanvas.SetActive(true);
       PauseCanvas.SetActive(false);
       Time.timeScale = FindObjectOfType<Player>().SlowTime ? 0.5f : 1f;
    }

    private void pauseButton()
    {
       UiCanvas.SetActive(false);
       PauseCanvas.SetActive(true);
       Time.timeScale = 0;
    }
    
    private void pillsButton()
    {
        if (PillsGO == null)
        {
            PillsGO = GameObject.FindGameObjectWithTag("PillsCanvas");
        }
        PillsGO.SetActive(PillsGO.activeSelf ? false : true);
        PlayerPrefs.SetInt("PillsMenu", PillsGO.activeSelf ? 1 : 0);
        
    }

    private void restartButton()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    private void goToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene ("MainMenu");
    }
}
