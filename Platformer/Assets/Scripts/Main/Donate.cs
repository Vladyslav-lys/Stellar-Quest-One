using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : MonoBehaviour
{
    public GameObject DonateCanvas;
    void Start()
    {
        DonateCanvas.SetActive(false);
    }

    public void ShowDonate()
    {
        DonateCanvas.SetActive(true);
    }

    public void HideDonate()
    {
        DonateCanvas.SetActive(false);

    }
}
