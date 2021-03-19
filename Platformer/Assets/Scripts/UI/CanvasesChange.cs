using UnityEngine;

public class CanvasesChange : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject SecondCanvas;
    public AudioClip ButtonSelect;
    
    public void ChangeCanvases()
    {
        FirstCanvas.SetActive(false);
        SecondCanvas.SetActive(true);
        AudioSource.PlayClipAtPoint (ButtonSelect, transform.position);

        foreach (var Skin in FindObjectsOfType<ChangeSkin>())
        {
            Skin.ChangeText();
        }
        
    }
}
