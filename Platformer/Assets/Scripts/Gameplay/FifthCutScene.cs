using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FifthCutScene : MonoBehaviour
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
        CameraSize = 0.19f;
        _go = true;
        StartCoroutine(ChangeCameraSize());
    }

    private void Update()
    {
        if (_go)
            Move();
       
        StartButton.SetActive(transform.position == Points[2].position);  
        if (transform.position == Points[2].position)
        {
            StartCoroutine(GoToFirstLevelCorutine());
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[_index].position, StartSpeed);
        
        if (transform.position == Points[_index].position && transform.position != Points[2].position)
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
            if (_go)
            {
               Camera.orthographicSize += CameraSize;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
     
    
    IEnumerator GoToFirstLevelCorutine()
    {
       
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("Lava_10");

    }
    
    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("Lava_10");
    }
}
