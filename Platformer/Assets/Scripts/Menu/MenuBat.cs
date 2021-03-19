using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBat : MonoBehaviour
{
    public Transform[] Points;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
    protected Vector2 _dir;
    protected Animator _animator;
    protected  IEnumerator<Transform> _currentPoint;
    protected bool _isFacingRight = true;
    private int _direction;

    // Start is called before the first frame update
    void Start()
    {
        _dir = new Vector2(-1, 0);
        _animator = GetComponent<Animator>();
        _currentPoint = GetPathsEnumerator();
        _currentPoint.MoveNext();
        transform.position = _currentPoint.Current.position;
        Flip();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Speed * Time.deltaTime); 
        
        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            Flip();
            _currentPoint.MoveNext();
        }
    }
    
    private IEnumerator<Transform> GetPathsEnumerator()
    {
        if (Points == null || Points.Length < 1)
            yield break;
        
        _direction = 1;
        var index = 0;
        
        while (true) {
            yield return Points[index];

            if(Points.Length == 1)
                continue;
            if(index <= 0)
                _direction = 1;
            else if (index >= Points.Length - 1)
                _direction = -1;

            index = index + _direction;
        }
    }
    
    private void Flip()
    {  
        _dir = -_dir;
        _isFacingRight = !_isFacingRight;
        transform.rotation *= Quaternion.Euler(0, _isFacingRight ? 180f : -180f, 0);
    }
}
