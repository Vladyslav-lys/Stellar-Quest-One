using UnityEngine;

public class IOSFix : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS
       transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z); 
#endif
    }
}
