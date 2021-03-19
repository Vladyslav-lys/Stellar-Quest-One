using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController2D _controller;
    private float _canFireIn;
    private int _health = 100;
    private int _energy;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
    public bool IsFacingRight;
    public bool IsCrouch;
    public float Move;
    public float MaxSpeed;
    public Animator Animator;
    public SimpleProjectile _projectile;
    public float FireRate;
    public Transform ProjectileFireLocation;
    public bool SlowTime = false;
    public GameObject TakesDamageEffect;
    public GameObject FireEffect;
    public bool AddHero = false;
    public float TrapRateTime;
    public bool StopPlayerTraps;
    public bool Fire;
    private SpriteRenderer _spriteRenderer;
    public bool IsDead { get; private set; }
    public int Health {   get { return _health; }set { _health = value; }}
    public int StartHealth;
    public int Energy
    {
        get { return _energy; }
        set {PlayerPrefs.SetInt("Energy", value); }
    }
    public AudioClip DeathSound;
    
    private void Awake()
    {     
        // if(GameObject.FindGameObjectWithTag("CruchButton"))
        //     GameObject.FindGameObjectWithTag("CruchButton").SetActive(PlayerPrefs.GetString("Skin") != "Char_5");

        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        FloatingText.Show (string.Format (""), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 2f, 60f));
        Animator = gameObject.GetComponent<Animator>();
        if (!PlayerPrefs.HasKey ("Skin")) 
        {
            PlayerPrefs.SetString("Skin", "Char_1");
        } 
        Animator.runtimeAnimatorController = Resources.Load(PlayerPrefs.GetString("Skin")) as RuntimeAnimatorController;
        _energy = PlayerPrefs.GetInt("Energy");
        _controller = GetComponent<CharacterController2D> ();
        IsFacingRight = transform.localScale.x > 0;
        Time.timeScale = 1;
        IsCrouch = false;
        SetParams();
    }

    private void Update()
    {
        _energy = PlayerPrefs.GetInt("Energy");
        Animator.SetBool ("IsDead", IsDead);
        Animator.SetBool("IsGrounded", _controller.State.IsCollidingBelow);
        
        if(TrapRateTime > 0)
            TrapRateTime -= Time.deltaTime;
        
        if (Move < 0 && IsFacingRight)
        {
            Flip();
        }
        if (Move > 0 && !IsFacingRight)
        {
            Flip();
        }
        
        ResizeCollider();
            
        if (!IsDead)
            HandleInput();
                
        if(_canFireIn > 0)
            _canFireIn -= Time.deltaTime;

        if (_canFireIn > 0 || IsDead || !Fire)
            return;

        var direction = IsFacingRight ? Vector2.right : -Vector2.right;
        var projectile = (SimpleProjectile)Instantiate(
            Resources.Load(PlayerPrefs.GetString("Skin") == "Char_5" ? "CharlieBullet" : PlayerPrefs.GetString("Bullet"), typeof(SimpleProjectile)) as SimpleProjectile,
            PlayerPrefs.GetString("Skin") == "Char_5" ? new Vector3(
                ProjectileFireLocation.transform.position.x, ProjectileFireLocation.transform.position.y + 1.7f, 0) :
                ProjectileFireLocation.position,
            ProjectileFireLocation.rotation);

        projectile.Initialize(gameObject, direction, _controller.Velocity);

        _canFireIn = FireRate;

        Animator.SetTrigger("Fire");
    }

    void FixedUpdate()
    {
        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        if (IsDead)
        {
        	_controller.SetHorizontalForce (0);
        }
        else 
        {
            _controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, MaxSpeed * Move, Time.deltaTime * movementFactor));
            Animator.SetFloat ("Speed", Mathf.Abs (_controller.Velocity.x) / MaxSpeed);
        }
    }
    
    public void RespawnAt (Transform spawnPoint)
    {
        if (!IsFacingRight)
            Flip ();
  
        StopPlayerTraps = false;
        transform.position = spawnPoint.position;     
        IsDead = false;
        _health = StartHealth;
        GetComponent<Collider2D> ().enabled = true;
    }
    
    public void Kill()
    {
        if(DeathSound != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (DeathSound, transform.position);

        _health = 0;
        Move = 0;
        IsDead = true;
        //if(PlayerPrefs.GetInt("EndlessEnergy") != 1)
        //    _energy--;
       
        SlowTime = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Energy", _energy);
        Animator.SetFloat ("Speed", 0);
        GetComponent<Collider2D>().enabled = false;
        _controller.SetForce(new Vector2(0, 0));
    }

    public void Walk (int speed)
    {
        if (IsDead)
            return;

        if (IsCrouch)
        {
            IsCrouch = false;
            Animator.SetBool("IsCrouch", false);    
        }
        
        Move = speed;
    }

    public void Jumping ()
    {
        if (_controller.CanJump && !IsDead)
        {
            if (IsCrouch)
            {     
                IsCrouch = false;
                Animator.SetBool("IsCrouch", false);
            }
            else
            {
               _controller.Jump (); 
            }
        }
    }

    private void HandleInput ()
    {
        if (Input.GetKey (KeyCode.D)) {
            if (IsCrouch)
            {
                IsCrouch = false;
                Animator.SetBool("IsCrouch", false);    
            }
            Move = 1;
            if (!IsFacingRight)
                    Flip ();
        }
        else if (Input.GetKey (KeyCode.A)) 
        {
            if (IsCrouch)
            {
                IsCrouch = false;
                Animator.SetBool("IsCrouch", false);    
            }
            Move = -1;
            if (IsFacingRight)
                    Flip ();
        }
        
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
        {
            Move = 0;
        }

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCrouch)
            {
                IsCrouch = false;
                Animator.SetBool("IsCrouch", false);
            }
            else
            {
                _controller.Jump (); 
            }
        }
        
        if (Input.GetKey (KeyCode.LeftShift))
            FireProjectile (true);  
        
        if (Input.GetKey (KeyCode.LeftControl))
            Crouch();
        
    }

    public void Crouch()
    {
        if (IsCrouch)
        {
            Animator.SetBool("IsCrouch", false);
            IsCrouch = false;
            return;
        }
        
        Move = 0;
        IsCrouch = true;
        Animator.SetBool("IsCrouch", true);
    }
    
    public void FireProjectile (bool fire)
    {
        Fire = fire;
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        IsFacingRight = transform.localScale.x > 0;
        if (!IsFacingRight)
        {
             transform.localPosition = new Vector3(transform.localPosition.x - (PlayerPrefs.GetString("Skin") == "Char_4" ? 3.5f : 1.5f), transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + (PlayerPrefs.GetString("Skin") == "Char_4" ? 3.5f : 1.5f), transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void TakeDamage(int damage)
    {
        Vector3 position = transform.position;
        position.y = position.y + 4;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, position, 2f, 60f));
        
        Health -= damage;
        Instantiate (TakesDamageEffect, transform.position, transform.rotation);

        if (Health <= 0)
        {
            StopPlayerTraps = true;
            FindObjectOfType<LevelManager>().KillPlayer ();
        }
    }

    public void GiveTrapsDamage()
    {
        TakeDamage(40);
        StartCoroutine(PlayerTraps());
        TrapRateTime = 2.5f;
    }

    IEnumerator PlayerTraps()
    {
        StartCoroutine(StopAfter4Sec());
        for(int i = 255; i > 1; i += i > 150 ? -2 : 100)
        {
            if (StopPlayerTraps)
            {
                StopPlayerTraps = false;
                _spriteRenderer.color = new Color32(255, 255, 255, 255);
                break; 
            }
            
            _spriteRenderer.color = new Color32(170, 239, 255, (byte)i);
            yield return new WaitForSecondsRealtime(0.002f);
        }
    }
    
    IEnumerator StopAfter4Sec()
    {
        if (IsDead)
            yield break;
        yield return new WaitForSecondsRealtime(2.5f);

        StopPlayerTraps = true;
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == ("Coin"))
        {
            LevelManager.Instance.Coins++;
        }
    }
    
    private void SetParams()
    {
        if (PlayerPrefs.GetString("Skin") == "Char_1")
        {
            StartHealth = 100;
            FireRate = 0.35f;
        }
        else if (PlayerPrefs.GetString("Skin") == "Char_2" )
        {
            StartHealth = 120;
            FireRate = 0.35f;
        }
        else if (PlayerPrefs.GetString("Skin") == "Char_3" )
        {
            StartHealth = 130;
            FireRate = 0.3f;
        }
        else if (PlayerPrefs.GetString("Skin") == "Char_4" )
        {
            StartHealth = 110;
            FireRate = 0.25f;
        }
        else if (PlayerPrefs.GetString("Skin") == "Char_5" )
        {
            StartHealth = 120;
            FireRate = 0.2f;
        }

        Health = StartHealth;
    }
    
    public void ResizeCollider()
    {
        if (IsCrouch)
        {
            FireRate = 0.2f;
            if (PlayerPrefs.GetString("Skin") == "Char_2" )
            {
                _controller._boxCollider.size = new Vector2(3.51f, 3.55f);
                _controller._boxCollider.offset = new Vector2(-0.81f, -0.6f); 
                ProjectileFireLocation.localPosition = new Vector3(4f, -1f, 0f); 
               
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_3")
            {
                _controller._boxCollider.size = new Vector2(3.51f, 4.3f);
                _controller._boxCollider.offset = new Vector2(-0.81f, -0.6f); 
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -1f, 0f);   
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_4")
            {
                _controller._boxCollider.size = new Vector2(3.05f, 4.5f);
                _controller._boxCollider.offset = new Vector2(-1.25f, -0.25f);
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -1f, 0f);
            }
            else
            {
                _controller._boxCollider.size = new Vector2(3.47f, 4.5f);
                _controller._boxCollider.offset = new Vector2(-0.23f, -0.4f);
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -1f, 0f);   
            }
        }
        else
        {
            if (AddHero)
                return;
            
            if (PlayerPrefs.GetString("Skin") == "Char_2")
            {
                FireRate = 0.35f;
                _controller._boxCollider.size = new Vector2(3.51f, 4.8f);
                _controller._boxCollider.offset = new Vector2(-0.81f, 0.1f);
                ProjectileFireLocation.localPosition = new Vector3(4f, -0.2f, 0f);  
            }  
            else if (PlayerPrefs.GetString("Skin") == "Char_3")
            {
                FireRate = 0.3f;
                _controller._boxCollider.size = new Vector2(3.51f, 5.36f);
                _controller._boxCollider.offset = new Vector2(-0.81f, 0f);
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -0.5f, 0f);
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_4")
            {
                FireRate = 0.25f;
                _controller._boxCollider.size = new Vector2(3.05f, 5.55f);
                _controller._boxCollider.offset = new Vector2(-1.6f, -0.32f); 
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -0.25f, 0f);  
            }
            else if (PlayerPrefs.GetString("Skin") == "Char_5")
            {
                _controller._boxCollider.size = new Vector2(3.41f, 4.28f);
                _controller._boxCollider.offset = new Vector2(-1.1f, -0.11f); 
                ProjectileFireLocation.localPosition = new Vector3(1.35f, -1.33f, 0f);  
            }
            else
            {
                FireRate = 0.35f;
                _controller._boxCollider.size = new Vector2(3.47f, 5.5f);
                _controller._boxCollider.offset = new Vector2(-0.23f, 0.11f); 
                ProjectileFireLocation.localPosition = new Vector3(2.56f, -0.1f, 0f);     
            }
        }
    }

    public void EndSlow()
    {
        SlowTime = true;
        StartCoroutine(EndSlowTime()); 
    }
    
    public IEnumerator EndSlowTime()
    {
        yield return new WaitForSeconds(10);
        SlowTime = false;
        Time.timeScale = 1;
    }
} 