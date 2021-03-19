using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic : MonoBehaviour
{
    public bool IsMenu = false;
    void Update()
    {
        if (IsMenu)
            return;
        
        if (FindObjectOfType<Player>().IsDead)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
