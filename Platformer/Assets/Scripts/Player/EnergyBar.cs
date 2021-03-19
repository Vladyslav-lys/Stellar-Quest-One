using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public Player Player;
    public Transform ForegroundSprite;

    public void Update()
    {	
        var healthPercent = Player.Energy / (float)PlayerPrefs.GetInt("MaxEnergy");

        ForegroundSprite.localScale = new Vector3 (healthPercent, 1, 1);

    }
}