using UnityEngine;

public class CripperGroundEnemy :  AbstractGroundEnemies
{
    public GameObject CripperEffect;
    private Vector2 
        _lastPosition,
        _velocity;

    public AudioClip CripperSound;
    
    private void LateUpdate()
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
    }
    
    protected override void AttackPlayer()
    {
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);
        
        if (attackPlayer.collider)
        {
            _player.TakeDamage(Damage);
            var controller = _player.GetComponent<CharacterController2D>();
            var totalVelocity = controller.Velocity + _velocity;

            controller.SetForce(new Vector2(
                -1 * Mathf.Sign (totalVelocity.x) * Mathf.Clamp (Mathf.Abs(totalVelocity.x) * 4, 8, 15),
                -1 * Mathf.Sign (totalVelocity.y) * Mathf.Clamp (Mathf.Abs(totalVelocity.y) * 4, 8, 15)));
            
            if(CripperSound != null && PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (CripperSound, transform.position);

            Instantiate (CripperEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}