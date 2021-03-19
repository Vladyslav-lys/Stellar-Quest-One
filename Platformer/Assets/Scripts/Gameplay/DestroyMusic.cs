using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    void Start()
    {
        if(AudioSourceMenu.Instance != null)
            AudioSourceMenu.Instance.Destroy();
    }
}
