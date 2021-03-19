using UnityEngine;

public class LinkButton : MonoBehaviour
{
    public string Link;
    
    public void OpenLink()
    {
        Application.OpenURL(Link);
    }
}
