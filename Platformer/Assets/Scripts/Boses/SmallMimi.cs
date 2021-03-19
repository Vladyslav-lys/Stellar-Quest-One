using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmallMimi : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{
    public float AttackRate;
    public int Damage;
    public float SearchPlayerDistance;
    public float SearchGroundDistance;
    public float SearchPlayerDistanceFlip;
    public float AttackPlayerDistanceCast;
    public float AttackPlayerDistanceMelee;
    public float StartSpeed;
    public float AttackSpeed;
    public float AttackSpeedMelee;
    public int Health;
    public LayerMask PlayerMask;
    public LayerMask GroundMask;
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
        Time.timeScale = 1;
    }

    public void LateUpdate()
    {
        if (_player.IsDead)
            _speed = StartSpeed;
        
        if(_player.IsDead)
            _health = Health;
        
        _animator.SetFloat("Speed", _speed);
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
        RaycastHit2D checkGround = Physics2D.Raycast(new Vector2(transform.position.x, gameObject.transform.position.y),
            _isFacingRight ? Vector2.right : Vector2.left, SearchGroundDistance, GroundMask);
        
        if (checkGround.collider)
        {
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
        if (_isDead || _player.IsDead)
            return;
        
        RaycastHit2D searchPlayer = Physics2D.Raycast(new Vector2(transform.position.x, gameObject.transform.position.y),
            _isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);  
        
       
        RaycastHit2D searchPlayerFlip = Physics2D.Raycast(new Vector2(
                _isFacingRight ? gameObject.transform.position.x - 5 : gameObject.transform.position.x + 5, gameObject.transform.position.y),
            _isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);

        if (!searchPlayer.collider)
        {
            searchPlayer = Physics2D.Raycast(new Vector2(transform.position.x, gameObject.transform.position.y - 3),
                _isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);  
        }

        if (!searchPlayerFlip)
        {
            searchPlayerFlip = Physics2D.Raycast(new Vector2(
                    _isFacingRight ? gameObject.transform.position.x - 5 : gameObject.transform.position.x + 5, gameObject.transform.position.y - 3),
                _isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);
        }
       
        
        if (searchPlayerFlip.collider)
            Flip();
        
        if (searchPlayer.collider)
        {
            if (_canFireIn > 2)
                return;
            
            if (_canFireIn > 0)
            {
                AttackMelle();
                _speed = AttackSpeedMelee;
                _seePlayer = true;
                return;
            }
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
            if (_canFireIn < 2f)
            {
                _speed = StartSpeed;
                _seePlayer = false;
            }          
        }
    }

   private void AttackMelle()
   {
       if (_canAttack > 0)
           return;
       
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceMelee, PlayerMask);
             
        if (!attackPlayer.collider)
        {
            attackPlayer = Physics2D.Raycast(new Vector2(transform.position.x, gameObject.transform.position.y - 3),
                _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceMelee, PlayerMask);  
        }
        
        if (attackPlayer.collider)
        {
            _canAttack = AttackRate;
            _canFireIn = FireRate + 1;
            _speed = AttackSpeed;
            _animator.SetTrigger("Melee");
            _player.TakeDamage(Damage);
        }

        _speed = StartSpeed;
   }
    
    private void Flip()
    {
        if (_canFlipSearch > 0)
            return;
        
        _canFlipSearch = WeitSearch;
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
        
        _canFireIn = FireRate;

        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceCast, PlayerMask);
           
        if (!attackPlayer.collider)
        {
            attackPlayer = Physics2D.Raycast(new Vector2(transform.position.x, gameObject.transform.position.y - 3),
                _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceCast, PlayerMask);  
        }
        
        if (attackPlayer.collider)
        {
            _speed = AttackSpeed;
            StartCoroutine(Fire());
        }
        else
        {
            _speed = StartSpeed;
        }
    }

    IEnumerator Fire()
    {
        _animator.SetTrigger("Shoot");

        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 2; i++)
        {
            var projectile = (Projectile)Instantiate (Projectile, new Vector3(transform.position.x, transform.position.y + 0.7f, 0), transform.rotation);
            var projectile_1 = (Projectile)Instantiate (Projectile, new Vector3(transform.position.x, transform.position.y - 0.7f, 0), transform.rotation);
            projectile.Initialize (gameObject, _direction, _controller._velocity);
            projectile_1.Initialize (gameObject, _direction, _controller._velocity);
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
            _health = 0;
            _animator.SetBool("IsDead", true);
            _isDead = true;
            _controller._boxCollider.enabled = false;
            Time.timeScale = 0.3f;
            StartCoroutine(WaitDead());
        }
    }
    
    protected IEnumerator WaitDead()
    {
        _speed = 0;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        if (PlayerPrefs.GetString("Language") == "Russian")
            SceneManager.LoadScene("LastCutSceneRus");
        if (PlayerPrefs.GetString("Language") == "English")
            SceneManager.LoadScene("LastCutScene");
    }

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _health = Health;
    }
}