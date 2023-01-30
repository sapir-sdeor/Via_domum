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
    [SerializeField] private LayerMask ignoreLayer;

    [SerializeField] private LayerMask echoLayer;

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

    #endregion

    #region private
    
   // private static readonly Vector3 ScaleYoung = new(0.589166641f,0.465384871f,1);
    private bool _onDiamond;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private float _horizontal;
    private float _vertical;
    private bool _isFacingRight;
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
    private bool _isClimbing;

    #endregion

    #region readonly
    private readonly Vector3 _pos1Level2 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private readonly Vector3 _pos2Level2 = new(-3.63643527f,1.41309333f,0.0770537108f);

    private readonly Vector3 _pos1Level3 = new(4.11999989f,-3.81999993f,0.0770537108f);
    private readonly Vector3 _pos2Level3 = new(-4.80000019f, 1.70000005f, 0.0770537108f);

    private readonly int GROUND_LAYER = 6;
    private readonly int PLAYER1_LAYER = 9;
    private readonly int PLAYER2_LAYER = 8;
    private readonly int IGNORE_LAYER = 2;  
    private readonly int WATER_LAYER = 4;
    private static bool _enterLoadLevel;
    private bool _enterHole;
    private bool _exitHole;
    private bool ignoreCollision1, ignoreCollision2;
    private Collider2D coll1, coll2;

    #endregion
   
   
    private void Start()
    {
        _enterLoadLevel = false;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UIManager>();
        coll1= coll2 = gameObject.GetComponent<Collider2D>();
        // coll1 = gameObject.GetComponent<Collider2D>();
    }

    public int GETPlayerNumber()
    {
        return playerNumber;
    }

    
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (uiManager.isPause) return;
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.8f);
        if (context.performed && IsGrounded())
        {
            SetJumpAnimation();
        }
    }

    public void Jump2(InputAction.CallbackContext context)
    {
        if (uiManager.isPause) return;
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.8f);
        if (context.performed && IsGrounded())
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
        _animator.SetTrigger(Jump1);
        _animator.SetBool(Wait1, false);
        StartCoroutine(WaitSecondForJump());
    }

    public void JumpDown(InputAction.CallbackContext context)
    {
      
        if (context.performed )
        {
            ignoreCollision1 = true;
        }  
    }
    
    public void JumpDown2(InputAction.CallbackContext context)
    {
        print(ignoreCollision2 + " jump Down player 2");
        if (context.performed )
        {
            otherPlayer.ignoreCollision2 = true;
        }  
    }

    private void SetMoveAnimation(InputAction.CallbackContext context)
    {
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
        var groundPos = groundCheck.position;
        return Physics2D.OverlapCircle(groundPos, 0.2f, groundLayer) 
               || Physics2D.OverlapCircle(groundPos, 0.2f,ignoreLayer );
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
    }

    private void Update()
    {
        if (transform.position.y < -3.2f && LevelManager.GETLevel() == 1)
            EnterHole();
        if (LevelManager.GETLevel() == 1 && !_enterHole && transform.position.y > 4.2f && transform.position.x < -3.5f)
            ExitHole();
        if (IsGrounded()) _enterHole = false;
        _animator.SetBool(ONGround, IsGrounded());
        CheckFalling();
    }

    private void EnterHole()
    {
        _enterHole = true;
        transform.position = new Vector3(-4.160326f,4.28f,0.0417999998f);
        GameObject mushroom = GameObject.FindGameObjectWithTag("mushroom");
        mushroom.GetComponent<Collider2D>().enabled = true;
        mushroom.GetComponent<Animator>().SetTrigger("grow");
    }
    
    private void ExitHole()
    {
        if (GetComponent<changeSize>().GETLittle())
            transform.position = new Vector3(-1.05999994f,-2.83999991f,-5.3326149f);
        else
        {
            GameObject mushroomHole = GameObject.FindGameObjectWithTag("mushroomHole");
            mushroomHole.GetComponent<mushroomHole>().Grow = false;
        }
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
        }
        else if (other.gameObject.CompareTag("obstcale")&& _destroyObstacle)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == WATER_LAYER && playerNumber == 1)
        {
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER1_LAYER , false);
        }
        if (other.gameObject.layer == WATER_LAYER && playerNumber == 2)
        {
            Physics2D.IgnoreLayerCollision(IGNORE_LAYER,PLAYER2_LAYER , false);
        }
        if (ignoreCollision1 && (other.gameObject.layer & groundLayer) == 0&&
            (string.Compare(other.gameObject.name,"ground")!= 0))
        {
            ignoreCollision1 = false;
            StartCoroutine(FallDownAndCancel(other,1));
        }
        if (ignoreCollision2 && (other.gameObject.layer & groundLayer) == 0 &&  
            (string.Compare(other.gameObject.name,"ground")!= 0))
        {   
            print("fall player 2 collision");
            ignoreCollision2 = false;
            StartCoroutine(FallDownAndCancel(other,2));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")){
            if (_rigidbody) _rigidbody.velocity = Vector2.zero; 
        }
    }

    IEnumerator FallDownAndCancel(Collision2D other, int playerAction)
    {
        print("fall down");
        if (playerAction == 1)
        {
            Physics2D.IgnoreCollision(other.collider,gameObject.GetComponent<Collider2D>());
            coll1 = other.collider;
        }

        if (playerAction == 2)
        {
            print("fall player 2");
            coll2 = other.collider;
            Physics2D.IgnoreCollision(other.collider,gameObject.GetComponent<Collider2D>());
        }
        yield return new WaitForSeconds(0.05f);

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if( playerNumber==1 && other.collider != coll1)
           Physics2D.IgnoreCollision(coll1,gameObject.GetComponent<Collider2D>(),false);
        if( playerNumber==2 && other.collider != coll2)
           Physics2D.IgnoreCollision(coll2,gameObject.GetComponent<Collider2D>(),false);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("flower"))
        {
            GetComponent<Fly>().StartFlying(flyPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Button))
            ClickButton();
        else if (other.gameObject.CompareTag(Diamond))
            OnDiamond(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Diamond))
        {
            _animator.SetBool(Wait1, false);
            other.GetComponent<Animator>().SetBool("first", false);
            _onDiamond = false;
        }
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
        ICoreMechanic coreMechanic = mechanicFactory.CreateMechanic(other.gameObject.tag, light2D, echoLayer);
        coreMechanic.ApplyMechanic();
    }

    private void CollectStone(Collision2D other)
    {
        _audioSource.clip = collectPowerSound;
        _audioSource.Play();
        uiManager.CollectPowerPlayer(gameObject ,other.gameObject);
    }

    private void ClickButton()
    {
        if (LevelManager.GETLevel() == 1)
        {
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
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
        switch (scene.name)
        {
            case "Level1":
                break;
            case "Level2":
                gameManager.SetPosPlayer1(_pos1Level2);
                gameManager.SetPosPlayer2(_pos2Level2);
                if (playerNumber == 2)
                    gameObject.AddComponent<Fly>();
                
                break;
            case "Level3":
                gameManager.SetPosPlayer1(_pos1Level3);
                gameManager.SetPosPlayer2(_pos2Level3);
                if (playerNumber == 2)
                    Destroy(gameObject.GetComponent<Fly>());
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
