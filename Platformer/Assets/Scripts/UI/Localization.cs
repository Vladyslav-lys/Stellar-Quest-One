using TMPro;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public string English;
    public string Russian;

    void Start()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Russian;
        } 
        if (PlayerPrefs.GetString("Language") == "English")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = English;
        }
    }
}
