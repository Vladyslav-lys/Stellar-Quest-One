using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThirdCutScene : MonoBehaviour
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
    public string EnglishSentence;
    public string RussianSentence;

    void Start()
    {
        Camera = GetComponent<Camera>();
        _index = 0;
        CameraSize = 0.11f;
        _go = false;
        StartCoroutine(ChangeCameraSize());
        StartCoroutine(WaitSec(StartDelay));
    }

    private void Update()
    {
        if (_go)
        {
            Move();
        }
        StartButton.SetActive(transform.position == Points[3].position);    
        if (transform.position == Points[3].position)
        {
            StartCoroutine(GoToFirstLevelCorutine());
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[_index].position, StartSpeed);
    
        if (transform.position == Points[2].position)
        {
            CameraSize = 0.4f;
            StartSpeed = 0.1f;
        }
        
        if (transform.position == Points[3].position)
        {
            _go = false;
        }
        if (transform.position == Points[0].position && _rot)
        {
            _go = false;
            StartCoroutine(SetImage());
        }
        
        if (transform.position == Points[_index].position && transform.position != Points[3].position && transform.position != Points[0].position)
        {
            _go = false;
            StartCoroutine(WaitSec(PointDelay));
            _index++;
        }
    }
   
    IEnumerator WaitSec(float sec)
    {
        Debug.Log(_go);
        yield return new WaitForSecondsRealtime(sec);
        _go = true;
        Debug.Log(_go);

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
        for(byte i = 255; i > 0; i--)
        {
            StartText.color = new Color32(255, 255, 255, i);
            StartBG.color = new Color32(0, 0, 0, i);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        
        StartText.text = "";
        StartBG.color = new Color32(255, 255, 255, 0);
        _go = true;
        _rot = false;
        _index++;
    }

    IEnumerator SetImage()
    {
        for (byte i = 0; i < 254; i += 3)
        {
            StartBG.color = new Color32(0, 0, 0, i);
            StartText.color = new Color32(255, 255, 255, i);
            yield return new WaitForSecondsRealtime(0.0002f);
        }

        StartCoroutine(Type());
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
      SceneManager.LoadScene("Lava_1");

    }
    
    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("Lava_1");
    }
}
