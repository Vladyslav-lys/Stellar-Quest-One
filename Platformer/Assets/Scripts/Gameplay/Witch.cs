using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    public GameObject Zombatya;
    private bool _go = false;
    public AudioClip WitchAudio;
    
    void Start()
    {
        CastSpell();
    }

    void Update()
    {
        if (_go)
        {
            gameObject.transform.position = Vector3.MoveTowards (transform.position,  new Vector3(6.17f, -57.25f, 0f), Time.deltaTime * 1.5f);
        }
    }
    
    private void CastSpell()
    {
        StartCoroutine(WheitAudio());

    }

    private IEnumerator WheitAudio()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint (WitchAudio, transform.position);
        StartCoroutine(WeitCast());
    }
    private IEnumerator WeitCast()
    {
        Time.timeScale = 0.8f;
        gameObject.GetComponent<Animator>().SetTrigger("Spell");
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        _go = true;
        Zombatya.SetActive(true);
     //   gameObject.SetActive(false);
    }
}
