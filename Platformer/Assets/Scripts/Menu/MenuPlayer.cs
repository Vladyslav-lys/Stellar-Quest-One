using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    private CharacterController2D _controller;
    public Animator Animator;

    private void Start()
    {     
        Animator = gameObject.GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D> ();
        if (!PlayerPrefs.HasKey ("Skin")) 
        {
            PlayerPrefs.SetString("Skin", "Char_1");
        } 
        Animator.runtimeAnimatorController = Resources.Load(PlayerPrefs.GetString("Skin")) as RuntimeAnimatorController;
        Time.timeScale = 1;
        ResizeCollider();
    }

    void Update()
    {
        Animator.SetBool("IsGrounded", _controller.State.IsCollidingBelow);
    }
    
    void ResizeCollider()
    {
            
            if (PlayerPrefs.GetString("Skin") == "Char_2")
            {
                _controller._boxCollider.size = new Vector2(3.51f, 4.8f);
                _controller._boxCollider.offset = new Vector2(-0.81f, 0.1f);
            }  
            else if (PlayerPrefs.GetString("Skin") == "Char_3")
            {
                _controller._boxCollider.size = new Vector2(3.51f, 5.36f);
                _controller._boxCollider.offset = new Vector2(-0.81f, 0f);
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_4")
            {
                _controller._boxCollider.size = new Vector2(3.05f, 5.55f);
                _controller._boxCollider.offset = new Vector2(-1.6f, -0.32f); 
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_5")
            {
                _controller._boxCollider.size = new Vector2(3.41f, 4.28f);
                _controller._boxCollider.offset = new Vector2(-1.1f, -0.11f); 
            }
            else
            {
                _controller._boxCollider.size = new Vector2(3.47f, 5.5f);
                _controller._boxCollider.offset = new Vector2(-0.23f, 0.11f); 
            }
    }
}
