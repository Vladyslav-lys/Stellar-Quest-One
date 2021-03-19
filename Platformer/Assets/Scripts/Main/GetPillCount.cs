using TMPro;
using UnityEngine;

public class GetPillCount : MonoBehaviour
{
    public string PillName;
    public TextMeshProUGUI PillTMP;
    
    void Update()
    {
        PillTMP.text = "x " + PlayerPrefs.GetInt(PillName);
    }
}
