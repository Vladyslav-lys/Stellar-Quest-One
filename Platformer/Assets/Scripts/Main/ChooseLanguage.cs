using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLanguage : MonoBehaviour
{
   public void Russian()
   {
      PlayerPrefs.SetString("Language", "Russian");
      SceneManager.LoadScene("MainMenu");
   }
   public void English()
   {
      PlayerPrefs.SetString("Language", "English");
      SceneManager.LoadScene("MainMenu");
   }
}
