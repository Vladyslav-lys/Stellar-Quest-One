using TMPro;
using UnityEngine;

public class MenuCoins : MonoBehaviour
{
    public TextMeshProUGUI CoinsTMP;
    
    void Update()
    {
        CoinsTMP.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
