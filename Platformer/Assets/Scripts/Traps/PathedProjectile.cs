using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathedProjectile : MonoBehaviour, ITakeDamage
{
    private Transform _destination;
    private float _speed;
    public GameObject Effect;

    public void Initalize(Transform desination, float speed)
    {
        _destination = desination;
        _speed = speed;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);

        var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;
        if (distanceSquared > .01f * .01f)
            return;

        Instantiate (Effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    public void TakeDamage(int damage, GameObject instigator)
    {
        Instantiate (Effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    protected void OnCollision2DEnter(Collider2D other)
    {
        Instantiate (Effect, transform.position, transform.rotation);        
    }
}
