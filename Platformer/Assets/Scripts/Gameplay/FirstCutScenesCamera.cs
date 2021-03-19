using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstCutScenesCamera : MonoBehaviour
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
    private bool _rot = true;

    public Image StartBG;
    public TextMeshProUGUI StartText;
    public string RussianSentence;
    public string EnglishSentence;

    void Start()
    {
        Camera = GetComponent<Camera>();
        _index = 0;
        CameraSize = 0.15f;
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (_go)
            Move();
       
        StartButton.SetActive(transform.position == Points[4].position);  
        if (transform.position == Points[4].position)
        {
            StartCoroutine(GoToFirstLevelCorutine());
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[_index].position, StartSpeed);
        if (_rot)
        {
            StartCoroutine(ChangeRotation());
        }
        if (transform.position == Points[0].position)
        {
            StartCoroutine(ChangeCameraSize());
        }
        if (transform.position == Points[3].position)
        {
            CameraSize = 0.25f;
        }
        
        if (transform.position == Points[4].position)
        {
            _go = false;
        }
        
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
    
    IEnumerator Type()
    {
        string sentences = PlayerPrefs.GetString("Language") == "English" ? EnglishSentence : RussianSentence;
        foreach (var letter in sentences.ToCharArray())
        {
            StartText.text += letter;
            yield return new WaitForSecondsRealtime(0.002f);
        }

        StartCoroutine(ContinueType());
    }
    
    IEnumerator ContinueType()
    {
        for(byte i = 255; i > 1; i -= 2)
        {
            StartBG.color = new Color32(0, 0, 0, i);
            StartText.color = new Color32(255, 255, 255, i);
            yield return new WaitForSecondsRealtime(0.0002f);
        }
        StartCoroutine(WaitSec(StartDelay));
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
    
    IEnumerator ChangeRotation()
    {
        _rot = false;
        for (int i = 0; i < 75; i++)
        {
            Camera.transform.rotation *= Quaternion.Euler(0f,0f, -0.2f);
            yield return new WaitForSecondsRealtime(0.005f);
        }
    }
    
    IEnumerator GoToFirstLevelCorutine()
    {
       
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("Purple_1");

    }
    
    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("Purple_1");
    }
}
