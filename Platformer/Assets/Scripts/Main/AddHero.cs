using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHero : MonoBehaviour
{
    public GameObject Hero;
    public string HeroName;
    private Animator _animator;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AddElen"))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        _animator = Hero.GetComponent<Animator>();
        _animator.runtimeAnimatorController = Resources.Load(HeroName) as RuntimeAnimatorController;

        if (!Hero.activeSelf)
            return;

        Hero.GetComponent<CharacterController2D>()._boxCollider.size = new Vector2(3.51f, 3.55f);
        Hero.GetComponent<CharacterController2D>()._boxCollider.offset = new Vector2(-0.81f, -0.6f); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;
        
        Hero.SetActive(true);
        PlayerPrefs.SetInt("AddElen", 0);
    }
}
