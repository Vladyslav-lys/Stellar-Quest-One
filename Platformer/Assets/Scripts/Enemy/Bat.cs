using UnityEngine;

public class Bat : AbstractFlyEnemies
{
    public GameObject CripperEffect;
    private Vector2 
        _lastPosition,
        _velocity;
    public AudioClip CripperSound;
    public AudioClip DeathSound;

    public void LateUpdate()
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
    }
    
    protected override void AttackPlayer()
    {       
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            !_isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);

        if (attackPlayer.collider)
        {
            _player.TakeDamage(Damage);
            var controller = _player.GetComponent<CharacterController2D>();
            var totalVelocity = controller.Velocity + _velocity;

            controller.SetForce(new Vector2(
                -1 * Mathf.Sign (totalVelocity.x) * Mathf.Clamp (Mathf.Abs(totalVelocity.x) * 4, 12, 22),
                -1 * Mathf.Sign (totalVelocity.y) * Mathf.Clamp (Mathf.Abs(totalVelocity.y) * 4, 12, 22)));
            
            if(CripperSound != null && PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (CripperSound, transform.position);
            
            Instantiate (CripperEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    public override void TakeDamage(int damage, GameObject instigator)
    {
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText",
            new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));

        if(_health <= 0)
        {
            if(DeathSound != null && PlayerPrefs.GetInt("Audio") != 0)
                AudioSource.PlayClipAtPoint (DeathSound, transform.position);

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _animator.SetBool("IsDead", true);
            _isDead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitDead());
        }
    }
}