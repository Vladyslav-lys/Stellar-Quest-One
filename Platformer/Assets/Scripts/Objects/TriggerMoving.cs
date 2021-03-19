using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoving : MonoBehaviour
{
    public bool _collider = false;
    
    public bool getCollider()
    {
        return _collider;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        
        if (player == null)
            return;
        
        _collider = true;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        
        if (player == null)
            return;
        
        _collider = false;
    }
}
