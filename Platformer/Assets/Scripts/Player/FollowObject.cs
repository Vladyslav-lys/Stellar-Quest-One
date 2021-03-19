using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Vector2 Offset;
    public Transform Following;

    void Update ()
    {
        transform.position = Following.transform.position + (Vector3)Offset;
    }
}