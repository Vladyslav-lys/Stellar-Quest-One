using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw90Rotation : MonoBehaviour
{
    public float Speed;
    public float RightRotate;
    public float LeftRotate;
    private bool _rotateRight = true;
    void Update()
    {     
        if (transform.rotation.z < RightRotate && _rotateRight)
        {
            transform.Rotate(new Vector3 (0f, 0f, (Speed * 50) * Time.deltaTime)); 
        }
        else
        {
            _rotateRight = false;
            transform.Rotate(new Vector3 (0f, 0f, -((Speed * 50) * Time.deltaTime)));
            
            if (transform.rotation.z < LeftRotate)
                _rotateRight = true;
            
        }
    }
}
