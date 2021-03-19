using UnityEngine;

public class ShopShopTutorial : MonoBehaviour
{
    public GameObject ContinueShopTutorial;
    public GameObject EndShopTutorial;
    public GameObject StartShopTutorial;
    
    void Start()
    {
        if(!PlayerPrefs.HasKey("DeleteShop"))
           StartShopTutorial.SetActive(PlayerPrefs.GetInt("ShopTutorial") == 2);
        else
            Destroy(gameObject);
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ShopTutorial") == 3)
        {
            StartShopTutorial.SetActive(false);
            ContinueShopTutorial.SetActive(true);  
        }
        if (PlayerPrefs.GetInt("ShopTutorial") == 4)
        {
            ContinueShopTutorial.SetActive(false);
            EndShopTutorial.SetActive(true);  
        }
        if (PlayerPrefs.GetInt("ShopTutorial") == 5)
        {
            Destroy(gameObject); 
        }
        
    }
}
