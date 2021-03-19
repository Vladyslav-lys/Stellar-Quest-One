using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollider : MonoBehaviour
{
    private Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {       
        gameObject.GetComponent<BoxCollider2D>().enabled = !_player.IsDead;
    }
}
