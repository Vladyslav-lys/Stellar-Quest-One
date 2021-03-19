using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Transform Destination;
    public PathedProjectile Projectile;
    public float Speed;
    public float FireRate;
    private Vector2 _direction;
    private float _nextShotInSeconds;
    public GameObject Effect;
    
    public void Start()
    {
        _nextShotInSeconds = FireRate;
    }

    public void Update()
    {
        if ((_nextShotInSeconds -= Time.deltaTime) > 0)
            return;

        var projectile = (PathedProjectile)Instantiate (Projectile, transform.position, transform.rotation);
        projectile.Initalize (Destination, Speed);
        Instantiate (Effect, transform.position, transform.rotation);
        _nextShotInSeconds = FireRate;
    }
			
    public void OnDrawGizmos()
    {
        if (Destination == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine (transform.position, Destination.position);
    }
}
