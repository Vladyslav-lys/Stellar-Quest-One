using UnityEngine;

public class MenuShoptutorial : MonoBehaviour
{
    public GameObject ContinueShopTutorial;
    public GameObject StartShopTutorial;
    
    void Start()
    {
       StartShopTutorial.SetActive(PlayerPrefs.GetInt("ShopTutorial") == 1); 
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ShopTutorial") == 2)
        {
            StartShopTutorial.SetActive(false);
            ContinueShopTutorial.SetActive(true);  
        }
    }
}
