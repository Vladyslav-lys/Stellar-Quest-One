using UnityEngine;

public class SetTrigeredObject : MonoBehaviour
{
    public GameObject EnabledObject;

    private void Start()
    {
        EnabledObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnabledObject.SetActive(true);
    }
}