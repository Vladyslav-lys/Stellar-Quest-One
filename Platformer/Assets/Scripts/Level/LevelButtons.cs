using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    public bool _enabled;
    public GameObject Star_1;
    public GameObject Star_2;
    public GameObject Star_3;
    public GameObject LockIcon;
    
    void Start()
    {
        _enabled = PlayerPrefs.HasKey(gameObject.name);
       LockIcon.SetActive(!_enabled);
       
       Star_1.SetActive(PlayerPrefs.GetInt(gameObject.name) > 0);
       Star_2.SetActive(PlayerPrefs.GetInt(gameObject.name) > 1);
       Star_3.SetActive(PlayerPrefs.GetInt(gameObject.name) > 2);
    }

    public void LoadLevel()
    {
        if (_enabled)
        {
            SceneManager.LoadScene(gameObject.name);
        }
    }
}
