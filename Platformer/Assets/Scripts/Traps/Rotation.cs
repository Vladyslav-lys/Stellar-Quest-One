using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float Speed = 3f;

    void Update () 
    {
        transform.Rotate (new Vector3 (0f, 0f, (Speed * 50) * Time.deltaTime));
    }
}
