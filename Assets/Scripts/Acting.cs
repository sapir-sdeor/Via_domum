using System;
using System.Collections;
using CoreMechanic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Acting : MonoBehaviour
{
    #region SeializeField

    [SerializeField] private AudioClip collectPowerSound;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpHeight = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 flyPosition;
    [SerializeField] private Light2D[] light2D;
    [SerializeField] private Acting otherPlayer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private int playerNumber;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float fallingThreshold = -0.01f;
    
    #endregion

    #region string constant

    private const String Wait = "wait";
    private const String JumpMove = "jump";
    private const String Walk = "walk";
    private const String Button = "button";
    private const String Diamond = "diamond";
    private const String ActString = "act";
    

    #endregion

    #region private
    
   // private static readonly Vector3 ScaleYoung = new(0.589166641f,0.465384871f,1);
    private bool _onDiamond;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private float _horizontal;
    private float _vertical;
    private bool _isFacingRight;
    private static bool _removeEachOther;
    private PlayerMovement _inputAction;
    private Animator _animator;
    private bool falling;
    private static bool _destroyObstacle;
    
    private static readonly int Wait1 = Animator.StringToHash(Wait);
    private static readonly int Walk1 = Animator.StringToHash(Walk);
    private static readonly int Jump1 = Animator.StringToHash(JumpMove);
    private static readonly int ONGround = Animator.StringToHash("onGround");
    private static readonly int BelowOther = Animator.StringToHash("belowOther");
    private static readonly int Falling = Animator.StringToHash("falling");
    private bool _onRope;
    private bool _isClimbing;

    #endregion

    #region readonly
    private readonly Vector3 _pos1Level2 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private readonly Vector3 _pos2Level2 = new(-3.63643527f,1.41309333f,0.0770537108f);

    private readonly Vector3 _pos1Level3 = new(4.11999989f,-3.81999993f,0.0770537108f);
    private readonly Vector3 _pos2Level3 = new(-4.80000019f, 1.70000005f, 0.0770537108f);

    private readonly int GROUND_LAYER = 6;
    private readonly int PLAYER1_LAYER = 9;
    private readonly int PLAYER2_LAYER = 10;
    private readonly int IGNORE_LAYER = 2;
    private readonly int WATER_LAYER = 4;
    private static bool _enterLoadLevel;

    #endregion
   
   
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public int GETPlayerNumber()
    {
        return playerNumber;
    }

    public void Restart(InputAction.CallbackContext context)
    {
        levelManager.Restart();
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        print("should jump "+context.performed+" "+IsGrounded()+" "+
              _removeEachOther + !_rigidbody );
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        if (_onRope && _rigidbody)
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.8f);
            return;
        }
        if (context.performed && (IsGrounded() || (_removeEachOther && !_rigidbody)))
        {
            SetJumpAnimation();
        }
    }

    public void Jump2(InputAction.CallbackContext context)
    { 
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        if (_onRope && _rigidbody)
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.8f);
            return;
        }
        if (context.performed && (IsGrounded() || (_removeEachOther && !_rigidbody)))
        {
            SetJumpAnimation();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (gameObject.name != UIManager.PLAYER1) return; 
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return; 
        SetMoveAnimation(context);
    }

    public void Move2(InputAction.CallbackContext context)
    {
        if (gameObject.name != UIManager.PLAYER2) return;
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        SetMoveAnimation(context);
    }
    private void SetJumpAnimation()
    {
        removeOnEachOther();
        _animator.SetTrigger(Jump1);
        _animator.SetBool(Wait1, false);
        StartCoroutine(WaitSecondForJump());
    }

    public void JumpDown(InputAction.CallbackContext context)
    {
        print("Jump Down");
        if (context.performed )
        {
            // SetJumpAnimation();
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER1_LAYER , true);
        }  
    }
    
    public void JumpDown2(InputAction.CallbackContext context)
    {
        print("Jump Down");
        if (context.performed )
        {
            // SetJumpAnimation();
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER2_LAYER , true);
        }  
    }

    private void SetMoveAnimation(InputAction.CallbackContext context)
    {
        removeOnEachOther();
        _animator.SetBool(Wait1, false);
        _animator.SetBool(Walk1, true);
        _horizontal = context.ReadValue<Vector2>().x;
    }
    
    IEnumerator WaitSecondForJump()
    {
        yield return new WaitForSeconds(0.1f);
        if (_rigidbody)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpHeight);
        }
    }
    
    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(2f);
        _removeEachOther = false;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var tran = transform;
        Vector3 localScale = tran.localScale;
        localScale.x *= -1f;
        tran.localScale = localScale;

        if (tran.childCount > 2)
        {
            var vector3 = tran.GetChild(2).localScale;
            vector3.x *= -1f;
            tran.GetChild(2).localScale = vector3;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) ||
               gameManager.JumpEachOtherWhoUp() == playerNumber || Physics2D.OverlapCircle(groundCheck.position,
                   0.2f,IGNORE_LAYER );
    }


    private void FixedUpdate()
    {
        if (_rigidbody)  _rigidbody.velocity = new Vector2(_horizontal * speed, _rigidbody.velocity.y);
        switch (_isFacingRight)
        {
            case false when _horizontal > 0f:
            case true when _horizontal < 0f:
                Flip();
                break;
        }
        if (_horizontal == 0) _animator.SetBool(Walk1, false);
        if (!_onRope && _rigidbody) _rigidbody.gravityScale = 1f;

    }

    private void Update()
    {
        if (transform.position.y < -3.2f)
        {
            transform.position = new Vector3(-4.160326f,4.28f,0.0417999998f);
        }
        if (!_onRope && !otherPlayer._onRope && 
            (gameManager.JumpEachOtherWhoUp() == 1 && playerNumber == 2 || 
             gameManager.JumpEachOtherWhoUp() == 2 && playerNumber == 1) && !_removeEachOther)
        {
            setOnEachOther();
            _removeEachOther = true;
        }
        _animator.SetBool(ONGround, IsGrounded());
        CheckFalling();
    }

    private void CheckFalling()
    {
        if (_rigidbody && _rigidbody.velocity.y < fallingThreshold)
        {
            falling = true;
            _animator.SetBool(Falling, falling);
        }
        else
        {
            falling = false;
            _animator.SetBool(Falling, falling);
        }

    }

  

    private void setOnEachOther()
    {
        _animator.SetBool(BelowOther, true);
        otherPlayer.GetComponent<SpriteRenderer>().enabled = false;
        otherPlayer.transform.parent = transform;
        if (otherPlayer.GetComponent<Rigidbody2D>())
        {
            Destroy(otherPlayer.GetComponent<Rigidbody2D>());
        }
        otherPlayer.GetComponent<Animator>().enabled = false;

    }
    
    private void removeOnEachOther()
    {
        if (!_rigidbody)
        {
            _rigidbody = this.AddComponent<Rigidbody2D>();
            _rigidbody.freezeRotation = true;
            StartCoroutine(WaitSecond());
        }
        otherPlayer._animator.SetBool(BelowOther, false);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().enabled = true;
        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Button))
            ClickButton();
        
        else if (other.gameObject.name == "stone")
            CollectStone(other);
        
        else if (other.gameObject.CompareTag("light"))
        {
            Destroy(other.gameObject);
            Act(other.gameObject, null);
        }
        else if (other.gameObject.CompareTag("particle system"))
        {
            print("collide");    
        }
        else if (other.gameObject.CompareTag("obstcale")&& _destroyObstacle)
        {
            Destroy(other.gameObject);
        }
        print(other.gameObject.layer);
        if (other.gameObject.layer == WATER_LAYER && playerNumber == 1)
        {
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER1_LAYER , false);
        }
        if (other.gameObject.layer == WATER_LAYER && playerNumber == 1)
        {
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER2_LAYER , false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")){
            if (_rigidbody) _rigidbody.velocity = Vector2.zero; 
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Button))
            ClickButton();
        else if (other.gameObject.CompareTag(Diamond))
            OnDiamond(other.gameObject);
        else if (other.gameObject.CompareTag("roop"))
            _onRope = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Diamond))
        {
            _animator.SetBool(Wait1, false);
            other.GetComponent<Animator>().SetBool("first", false);
            _onDiamond = false;
        }
        else if (other.gameObject.CompareTag("roop"))
            _onRope = false;
    }

    public void Act(GameObject other, AudioClip audioClip)
    {
        if (audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
        MechanicFactory mechanicFactory = gameObject.GetComponent<MechanicFactory>();
        if (!mechanicFactory)
            mechanicFactory = gameObject.AddComponent<MechanicFactory>();
        ICoreMechanic coreMechanic = mechanicFactory.CreateMechanic(other.gameObject.tag,
            flyPosition, light2D);
        coreMechanic.ApplyMechanic();
    }

    private void CollectStone(Collision2D other)
    {
        _audioSource.clip = collectPowerSound;
        _audioSource.Play();
        uiManager.CollectPowerPlayer(gameObject ,other);
    }

    private void ClickButton()
    {
        if (LevelManager.GETLevel() == 1)
        {
            print("mushroom");
            gameManager.OpenGate();
            mushroom.GetComponent<Animator>().SetTrigger("Collision");
        }
    }
    
    private void OnDiamond(GameObject other)
    {
        _onDiamond = true;
        other.GetComponent<Animator>().SetBool("first", true);
        if (otherPlayer.getOnDiamond())
        {
            if (_removeEachOther)
            {
                _removeEachOther = false;
                removeOnEachOther();
                otherPlayer.removeOnEachOther();
            }
            _animator.SetBool(Wait1, true);
            if (GetComponent<changeSize>() && GetComponent<changeSize>().GETLittle())
            {
                GetComponent<changeSize>().ApplyMechanic();
            }
            other.GetComponent<Animator>().SetTrigger("second");
            if (!_enterLoadLevel) StartCoroutine(LoadLevelAfterSecond());
        }
        else
        {
            _animator.SetBool(Wait1, true); 
        }
    }

    IEnumerator LoadLevelAfterSecond()
    {
        _enterLoadLevel = true;
        yield return new WaitForSeconds(1f);
        levelManager.LoadNextLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Level1":
                break;
            case "Level2":
                levelManager.SetPosPlayer1(_pos1Level2);
                levelManager.SetPosPlayer2(_pos2Level2);
                break;
            case "Level3":
                levelManager.SetPosPlayer1(_pos1Level3);
                levelManager.SetPosPlayer2(_pos2Level3);
                // PlayerNumberInDownTunnel = 1;
                break;
        }
    }


    private bool getOnDiamond()
    {
        return _onDiamond;
    }

    public static void SetDestroyObstcale()
    {
        _destroyObstacle = true;
    }
}
