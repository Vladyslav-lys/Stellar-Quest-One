using UnityEngine;

public class ShopCanvases : MonoBehaviour
{
    public GameObject PlayerCanvas;
    public GameObject BulletCanvas;
    public GameObject PillCanvas;
    public AudioClip ButtonSelect;

    public void ShowPlayerCanvas()
    {
        AudioSource.PlayClipAtPoint (ButtonSelect, new Vector3(0,0,0), 1);
        PlayerCanvas.SetActive(true);
        BulletCanvas.SetActive(false);
        PillCanvas.SetActive(false);
    }
    
    public void ShowBulletCanvas()
    {
        if (PlayerPrefs.GetInt("ShopTutorial") == 2)
        {
            PlayerPrefs.SetInt("ShopTutorial", 3);
        }
        AudioSource.PlayClipAtPoint (ButtonSelect, new Vector3(0,0,0), 1);
        PlayerCanvas.SetActive(false);
        BulletCanvas.SetActive(true);
        PillCanvas.SetActive(false);
    }
    
    public void ShowPillCanvas()
    {
        AudioSource.PlayClipAtPoint (ButtonSelect, new Vector3(0,0,0), 1);
        PlayerCanvas.SetActive(false);
        BulletCanvas.SetActive(false);
        PillCanvas.SetActive(true);
    }
}