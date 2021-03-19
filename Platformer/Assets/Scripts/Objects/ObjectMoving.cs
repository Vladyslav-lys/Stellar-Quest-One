using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    public enum FollowType
    {
        MoveTowards,
        Lerp
    }
    
    public FollowType Type = FollowType.MoveTowards;
    public MainMoving Move;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
    public  IEnumerator<Transform> _currentPoint;
    private Vector3 _boundsCenter;

    public void Start()
    {
        if(Move == null)
            return;
        
        _currentPoint = Move.GetPathsEnumerator();
        _currentPoint.MoveNext();
        
        if (_currentPoint.Current == null)
            return;
        
        transform.position = _currentPoint.Current.position;
    }

    private void FixedUpdate()
    {
        if (Type == FollowType.MoveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Speed * Time.fixedDeltaTime);
        
        if (Type == FollowType.Lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        
        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            _currentPoint.MoveNext();
    }
}
