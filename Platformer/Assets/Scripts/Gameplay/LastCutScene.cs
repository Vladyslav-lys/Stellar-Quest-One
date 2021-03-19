using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCutScene : MonoBehaviour
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
       
        StartButton.SetActive(transform.position == Points[4].position);  
        if (transform.position == Points[4].position)
        {
            StartCoroutine(ChangeCameraSize());
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[_index].position, StartSpeed);
      
        if (transform.position == Points[_index].position && transform.position != Points[4].position)
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
            Camera.orthographicSize += CameraSize;           
            yield return new WaitForSecondsRealtime(0.02f);
        }

        StartCoroutine(GoToFirstLevelCorutine());
    }
     
    
    IEnumerator GoToFirstLevelCorutine()
    {
       
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("Titres");

    }
    
    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("Titres");
    }
}
