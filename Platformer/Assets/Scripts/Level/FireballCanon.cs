using UnityEngine;

public class FireballCanon : MonoBehaviour
{
    public Transform Destination;
    public Transform EndDestination;
    public DynamicBullet Projectile;
    public float Speed;
    private float FireRate;
    private Vector2 _direction;
    private float _nextShotInSeconds;
    
    public void Start()
    {
        _nextShotInSeconds = FireRate;
        FireRate = Random.Range(2f, 5f);
    }

    public void Update()
    {
        if ((_nextShotInSeconds -= Time.deltaTime) > 0)
            return;

        var projectile = (DynamicBullet)Instantiate (Projectile, transform.position, transform.rotation);
        projectile.Initalize (Destination,EndDestination, Speed);
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
