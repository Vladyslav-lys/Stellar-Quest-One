using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Player Player;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Color MaxHealthColor = Color.red;
    public Color MinHealthColor = Color.green;
    public TextMeshProUGUI HealthTMP;
    public void Update()
    {     
        float healthPercent = Player.Health / (float)Player.StartHealth;
        
        HealthTMP.text = Player.Health + " / " + Player.StartHealth;
        
        ForegroundSprite.localScale = new Vector3 (healthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp (MaxHealthColor, MinHealthColor, healthPercent);
    }
}