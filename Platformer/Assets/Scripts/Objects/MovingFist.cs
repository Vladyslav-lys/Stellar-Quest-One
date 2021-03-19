using System.Collections.Generic;
using UnityEngine;

public class MovingFist : MonoBehaviour
{
	public GameObject Point;
	public InstaKill Kill;
	public TriggerMoving ScriptCollider;
	bool lastCollision = false;
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	public FollowType Type = FollowType.MoveTowards;
	public MainMoving Move;
	public float SpeedUp = 1;
	public float SpeedDown = 2;
	public float MaxDistanceToGoal = .1f;
	public enum KillMove{Up,Down}
	public KillMove TypeKill = KillMove.Up;
	private  IEnumerator<Transform> _currentPoint;
	private  IEnumerator<Transform> _startPoint;
	float Speed;

	public void Start()
	{
		_startPoint = Move.GetPathsEnumerator();
		_startPoint.MoveNext ();
		_currentPoint = Move.GetPathsEnumerator();
		_currentPoint.MoveNext();
		
		if (_currentPoint.Current == null)
			return;
		
		transform.position = _currentPoint.Current.position;
	}
	
	public void Update ()
	{
		if (_currentPoint == null || _currentPoint.Current == null)
			return;
		
		var distanceSquared = (transform.position - Point.transform.position).sqrMagnitude;
		
		if (ScriptCollider.getCollider()) {
			if (!lastCollision)
			{								
				_currentPoint = Move.GetPathsEnumerator();
				_currentPoint.MoveNext();
				lastCollision = true;				
			}
			
			int dir = _currentPoint.Current.position.y > transform.position.y ? dir = 0 : dir = 1;

			if (dir == 1) {
				if (TypeKill == KillMove.Up)
				{
					Kill.setKill(true);
				}
				else
				{
					Kill.setKill(false);
				}
		
				Speed = SpeedUp;
			} else
			{
				if (TypeKill == KillMove.Up)
				{
					Kill.setKill(false);
				}
				else
				{
					Kill.setKill(true);
				}
				Speed = SpeedDown;
			}
			if (Type == FollowType.MoveTowards)
				transform.position = Vector3.MoveTowards (transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
			else if (Type == FollowType.Lerp)
				transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
			
			distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
			
			if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
				_currentPoint.MoveNext();
		}
		else
		{
			if (lastCollision)
			{
				lastCollision = false;
			}
			if (Type == FollowType.MoveTowards)
				transform.position = Vector3.MoveTowards (transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
			else if (Type == FollowType.Lerp)
				transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
			
			distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
			
			if ((distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) && (_currentPoint.Current.position != _startPoint.Current.position))
			{
				_currentPoint = Move.GetPathsEnumerator ();
				_currentPoint.MoveNext ();
				Speed = SpeedDown;
			}
		}
	}
}