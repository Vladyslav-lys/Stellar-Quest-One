using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mage : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{
    public float AttackRate;
    public int Damage;
    public float SearchPlayerDistance;
    public float SearchPlayerDistanceFlip;
    public float AttackPlayerDistanceCast;
    public float AttackPlayerDistanceMelee;
    public float StartSpeed;
    public float AttackSpeed;
    public float AttackSpeedMelee;
    public int Health;
    public LayerMask PlayerMask;
    public float WeitSearch = 1f;
    public Projectile Projectile;
    public float FireRate;
    public GameObject MageHealthBar;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private CharacterController2D _controller;
    private float _speed;
    private Vector2 _direction;
    private bool _isFacingRight;
    private bool _isDead = false;
    private Player _player;
    private int _health;
    private float _canAttack;
    private bool _seePlayer = false;
    private float _canFlipSearch;
    private float _canFireIn;
    
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Color MaxHealthColor = Color.red;
    public Color MinHealthColor = Color.green;
    
    public void Start()
    {
        _player = FindObjectOfType<Player>();
        _health = Health;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
    }

    public void Update()
    {
        if (_player.IsDead)
            _speed = StartSpeed;
        
        MageHealth();

        if (_canFireIn > 0)
            _canFireIn -= Time.deltaTime;
        
        if (_canFlipSearch > 0)
             _canFlipSearch -= Time.deltaTime;
        
        if (_canAttack > 0)
            _canAttack -= Time.deltaTime;
        
        _controller.SetHorizontalForce(_direction.x * _speed);
        _isFacingRight = transform.localScale.x > 0;
        _animator.SetFloat("Speed", _speed);
        
        CheckGround();
        SearchPlayer();
    }

    private void CheckGround()
    {
        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) ||
            (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            _canFlipSearch = WeitSearch;
            Flip();
        }   
    }
    
    
    private void MageHealth()
    {
        float healthPercent = _health / (float)Health;
                
        ForegroundSprite.localScale = new Vector3 (healthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp (MaxHealthColor, MinHealthColor, healthPercent);
    }
    
    private void SearchPlayer()
    {
        if (_isDead || _player.IsDead || _canAttack > 0 || _canFireIn > 0)
            return;
        
        RaycastHit2D searchPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);  
        
       
        RaycastHit2D searchPlayerFlip = Physics2D.Raycast(new Vector2(
                _isFacingRight ? gameObject.transform.position.x - 5 : gameObject.transform.position.x + 5, gameObject.transform.position.y),
            _isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);
        
        if (searchPlayerFlip.collider)
            Flip();
        
        if (searchPlayer.collider)
        {
            _seePlayer = true;
            _speed = AttackSpeedMelee;
            var random = Random.Range(0, 2);
            if (random == 1)
                AttackMelle(); 
            else 
                FireAttack();               
        }
        else
        {
            _speed = StartSpeed;
            _seePlayer = false;
        }
    }

   private void AttackMelle()
    {
        _canAttack = AttackRate;

        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceMelee, PlayerMask);
             
        if (attackPlayer.collider)
        {
            _speed = AttackSpeed;
            _animator.SetTrigger("Melee");
            _player.TakeDamage(Damage);
        }
    }
    
    private void Flip()
    {
        _direction = -_direction;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localPosition =
            new Vector3(!_isFacingRight ? transform.localPosition.x + 2f : transform.localPosition.x - 2f,
                transform.localPosition.y, transform.localPosition.z);
    }

    private void FireAttack()
    {
        if (_canFireIn > 0)
            return;
        
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceCast, PlayerMask);
             
        if (attackPlayer.collider)
        {
            _speed = AttackSpeed;
            StartCoroutine(Fire());
        }
        else
        {
            _speed = StartSpeed;
        }
        _canFireIn = FireRate;
    }

    IEnumerator Fire()
    {
        _animator.SetTrigger("Shoot");
        
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 3; i++)
        {
            var projectile = (Projectile)Instantiate (Projectile, new Vector3(transform.position.x, transform.position.y + 0.7f, 0), transform.rotation);
            projectile.Initialize (gameObject, _direction, _controller._velocity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public void TakeDamage(int damage, GameObject instigator)
    {
        if(!_seePlayer)
            Flip();
        
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));
        
        if(_health <= 0)
        {
            _animator.SetBool("IsDead", true);
            _isDead = true;
            _controller._boxCollider.enabled = false;
            Time.timeScale = 0.3f;
            StartCoroutine(WaitDead());
            MageHealthBar.SetActive(false);
        }
    }
    
    protected IEnumerator WaitDead()
    {
        _speed = 0;
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 3);
        
        if (!PlayerPrefs.HasKey("Forest_1"))
        {
            PlayerPrefs.SetInt("Forest_1", 0);
        }
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene("SecondCutSceneRus");
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene("SecondCutScene");
    }

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _health = Health;
    }
}