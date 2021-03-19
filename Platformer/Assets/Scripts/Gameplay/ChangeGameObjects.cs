using UnityEngine;

public class ChangeGameObjects : MonoBehaviour
{
    public GameObject FinishCanvas;
    public GameObject NotificationCanvas;

    void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetString("CompleteLevel") == "Purple_3");  
    }
    
    public void ChangeCanvases()
    {
        if (PlayerPrefs.GetString("CompleteLevel") == "Purple_3")
        {
            FinishCanvas.SetActive(false);
            NotificationCanvas.SetActive(true);
            PlayerPrefs.SetInt("ShopTutorial", 1);
        }  
    }
}
