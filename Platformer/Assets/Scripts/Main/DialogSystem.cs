using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public string[] Sentences;
    public string[] RussianSentences;
    public string[] EnglishSentences;
    public GameObject[] SentencesIcon;
    public GameObject[] AllSentences;
    public float TypingSpeed;
    private int _index;
    public GameObject ContinueButton;
    public GameObject SpeedButton;
    public GameObject DialogCanvas;
    public GameObject FireButton;
    public GameObject PauseButton;
    
    void Start()
    {       
        ContinueButton.SetActive(false);
        DialogCanvas.SetActive(false);
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            Sentences = RussianSentences;
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            Sentences = EnglishSentences;
        }
    }

    void Update()
    {
        if (TextDisplay.text == Sentences[_index])
            ContinueButton.SetActive(true);  
    }
    
    IEnumerator Type()
    {
        SpeedButton.SetActive(true);  
        foreach (var letter in Sentences[_index].ToCharArray())
        {
            ResetIcon();
            SentencesIcon[_index].SetActive(true);
            TextDisplay.text += letter;
            yield return new WaitForSecondsRealtime(TypingSpeed);
        }
        SpeedButton.SetActive(false);  
    }

    public void NextSentences()
    {
        ContinueButton.SetActive(false);
        TypingSpeed = 0.02f;
        if (_index < Sentences.Length - 1)
        {
            _index++;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            Time.timeScale = FindObjectOfType<Player>().SlowTime ? 0.5f : 1f;
            DialogCanvas.SetActive(false);
            FireButton.SetActive(true);
            PauseButton.SetActive(true);
            TextDisplay.text = "";
            Destroy(gameObject);
        }
    } 
    
    public void ChangeSpeed()
    {
       StopAllCoroutines();
       TextDisplay.text = Sentences[_index];
       SpeedButton.SetActive(false);  
    }

    private void ResetIcon()
    {
        foreach (var icon in AllSentences)
            icon.SetActive(false);   
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player" || collider2D.tag == "Mage")
        {
            Time.timeScale = 0;
            DialogCanvas.SetActive(true);
            FireButton.SetActive(false);
            PauseButton.SetActive(false);
            StartCoroutine(Type());
        }  
    }

    public void Skip()
    {
        Time.timeScale = FindObjectOfType<Player>().SlowTime ? 0.5f : 1f;
        DialogCanvas.SetActive(false);
        FireButton.SetActive(true);
        PauseButton.SetActive(true);
        TextDisplay.text = "";
        Destroy(gameObject);
    }
}
