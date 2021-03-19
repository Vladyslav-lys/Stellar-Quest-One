using UnityEngine;

public class DynamicBullet : MonoBehaviour
{
    private Transform _destination;
    private Transform _endDestination;
    private float _speed;

    public void Initalize(Transform desination,Transform endDestionation, float speed)
    {
        _destination = desination;
        _endDestination = endDestionation;
        _speed = speed;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);
        var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;
        if (distanceSquared < .01f * .01f)
        {
            _destination = _endDestination;
            transform.rotation *= Quaternion.Euler(0 , 0, 180);

            var distanceSquaredEnd = (_endDestination.transform.position - transform.position).sqrMagnitude;
            if (distanceSquaredEnd < .01f * .01f)
            {
                Destroy(gameObject);
            } 
        }        
    }
}
