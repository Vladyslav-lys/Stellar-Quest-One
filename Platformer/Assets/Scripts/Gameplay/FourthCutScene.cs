using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthCutScene : MonoBehaviour
{
    public Camera Camera;
    public Transform[] Points;
    public float StartDelay;
    public float PointDelay;
    public float StartSpeed;
    public GameObject StartButton;
    private float CameraSize;
    private int _index;
    private bool _go = false;
    private bool _stop = false;

    void Start()
    {
        Camera = GetComponent<Camera>();
        _index = 0;
        CameraSize = 0.15f;
        StartCoroutine(WaitSec(StartDelay));
    }

    private void Update()
    {
        if (_go)
            Move();
       
        StartButton.SetActive(transform.position == Points[3].position);  
        if (transform.position == Points[3].position)
        {
            StartCoroutine(GoToFirstLevelCorutine());
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[_index].position, StartSpeed);
      
        if (transform.position == Points[0].position)
        {
            StartCoroutine(ChangeCameraSize());
        }
        if (transform.position == Points[1].position)
        {
            _stop = true;
            StopCoroutine(ChangeCameraSize());
            CameraSize = 0.25f;
            StartCoroutine(ChangeCameraSizeLess());
        }
        
        if (transform.position == Points[2].position)
        {
            _stop = false;
            _go = false;
        }
        
        if (transform.position == Points[_index].position && transform.position != Points[3].position)
        {
            _go = false;
            StartCoroutine(WaitSec(PointDelay));
            _index++;
        }
    }
   
    IEnumerator WaitSec(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        _go = true;
    }  
    
    IEnumerator ChangeCameraSize()
    {
        while (Camera.orthographicSize < 65)
        {
            if (_go && !_stop)
            {
               Camera.orthographicSize += CameraSize;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
    
    IEnumerator ChangeCameraSizeLess()
    {
        while (Camera.orthographicSize > 15)
        {      
            if(_go && _stop)
                Camera.orthographicSize -= CameraSize;
            
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
     
    
    IEnumerator GoToFirstLevelCorutine()
    {
       
        yield return new WaitForSecondsRealtime(10f);
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene("FifthCutSceneRus");
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene("FifthCutScene");
    }
    
    public void GoToFirstLevel()
    {
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene("FifthCutSceneRus");
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene("FifthCutScene");
    }
}