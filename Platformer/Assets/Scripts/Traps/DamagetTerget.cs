using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagetTerget : MonoBehaviour, ITakeDamage
{
    public int Health = 50;
    public GameObject Effect;

    void Update()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
            Instantiate (Effect, transform.position, transform.rotation);
        }
    }
    
    public void TakeDamage(int damage, GameObject instigator)
    {
        Health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));
    }
}
