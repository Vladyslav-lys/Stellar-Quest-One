using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float JumpMagnitude;
    public AudioClip JumpSound;
    
    public void ControllerEnter2D(CharacterController2D controller)
    {
        if(JumpSound != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (JumpSound, transform.position);
        
        controller.SetVerticalForce(JumpMagnitude);
    }
}
