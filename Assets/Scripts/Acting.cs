using System;
using System.Collections;
using CoreMechanic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Touch = CoreMechanic.Touch;

public class Acting : MonoBehaviour
{
    #region SeializeField

    [SerializeField] private AudioClip collectPowerSound;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpHeight = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask echoLayer;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private Light2D[] light2D;
    [SerializeField] private Acting otherPlayer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private int playerNumber;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float fallingThreshold = -0.01f;
    [SerializeField] private float holeLimit=-3.1f;
    
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
    private bool hasTouch;
    private bool _onDiamond;
    private bool _onLeaf;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private float _horizontal;
    private float _vertical;
    private bool _isFacingRight;
    private PlayerMovement _inputAction;
    private Animator _animator;
    private bool falling;
    private bool player1Little = false, player2Little= false;
    private static bool _destroyObstacle;
    
    private static readonly int Wait1 = Animator.StringToHash(Wait);
    private static readonly int Walk1 = Animator.StringToHash(Walk);
    private static readonly int Jump1 = Animator.StringToHash(JumpMove);
    private static readonly int ONGround = Animator.StringToHash("onGround");
    private static readonly int Falling = Animator.StringToHash("falling");
    private bool _isClimbing;

    public bool gotHole;
    #endregion

    #region readonly
    private readonly Vector3 _pos1Level2 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private readonly Vector3 _pos2Level2 = new(-3.63643527f,1.41309333f,0.0770537108f);
    
    private readonly Vector3 _pos1Tutorial2 = new(0.689999998f,-2.31999993f,0.0770900697f);
    private readonly Vector3 _pos2Tutorial2 = new(1.25f,-2.21000004f,0.0770900697f);

    private readonly Vector3 _pos1Level1 = new(-0.730000019f, 0.540000021f, 0.0770900697f);
    private readonly Vector3 _pos2Level1 = new(-1.36000001f, 0.419999987f, 0.0770900697f);
    
    private readonly Vector3 _pos1Level3 = new(4.11999989f, -1.65999997f, 0.0770537108f);
    private readonly Vector3 _pos2Level3 = new(-4.80000019f, 1.70000005f, 0.0770537108f);
  
    
    private static bool _enterLoadLevel;
    private bool _enterHole;
    private bool _exitHole;
    private bool ignoreCollision1, ignoreCollision2;
    private Collider2D coll1, coll2;
    private GameObject _light1,_light2;
    // private Vector3 _shrinkPosLeft = new(-4.94665909f,4.01503992f,-0.0584629141f);
    private Vector3 _shrinkPosRight = new (5.05000019f,-1.16999996f,-0.0397099368f);

    #endregion
   
   
    private void Start()
    {
        player1Little = false;
        player2Little = false;
        _enterLoadLevel = false;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UIManager>();
        coll1 = coll2 = gameObject.GetComponent<Collider2D>();
        hasTouch = false;
    }

    public int GETPlayerNumber()
    {
        return playerNumber;
    }

    public void Restart(InputAction.CallbackContext context)
    {
        hasTouch = false;
        otherPlayer.hasTouch = false;
        levelManager.Restart();
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
        if (uiManager.isPause || !GetComponent<Collider2D>().enabled ) return;
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
        if (gameObject.name != UIManager.PLAYER1 ) return; 
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return; 
        SetMoveAnimation(context);
    }

    public void Move2(InputAction.CallbackContext context)
    {
        if (gameObject.name != UIManager.PLAYER2 ) return;
        if (!GetComponent<Collider2D>().enabled) return;
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
        if (transform.position.y <= holeLimit && LevelManager.GETLevel() == 1 &&
            GetComponent<changeSize>())
            EnterHole();
        if (LevelManager.GETLevel() == 1 && !_enterHole && transform.position.y > 4.2f && transform.position.x < -3.5f)
            ExitHole();
        if (IsGrounded()) _enterHole = false;
        _animator.SetBool(ONGround, IsGrounded());
        CheckFalling();
    }

    private void EnterHole()
    {
        gotHole = true;
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

    public bool GetTouch()
    {
        return hasTouch;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Button))
            ClickButton();
        
        else if (other.gameObject.name == "stone")
        {
            if (!hasTouch && other.gameObject.CompareTag("touch"))
            {
                CollectStone(other); 
            }
            else if (!other.gameObject.CompareTag("touch"))
            {
                CollectStone(other); 
            }
        }

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
        if (ignoreCollision1 && (other.gameObject.layer & groundLayer) == 0&&
            (string.Compare(other.gameObject.name,"ground")!= 0)&& other.gameObject.CompareTag("ignore"))
        {
            ignoreCollision1 = false;
            StartCoroutine(FallDownAndCancel(other,1));
        }
        if (ignoreCollision2 && (other.gameObject.layer & groundLayer) == 0 &&  
            (string.Compare(other.gameObject.name,"ground")!= 0)&& other.gameObject.CompareTag("ignore"))
        {
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
        if (playerAction == 1)
        {
            Physics2D.IgnoreCollision(other.collider,gameObject.GetComponent<Collider2D>());
            coll1 = other.collider;
        }
        if (playerAction == 2)
        {
            coll2 = other.collider;
            Physics2D.IgnoreCollision(other.collider,gameObject.GetComponent<Collider2D>());
        }
        yield return new WaitForSeconds(0.05f);

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(playerNumber==1 && other.collider != coll1 && coll1 != null)
           Physics2D.IgnoreCollision(coll1,gameObject.GetComponent<Collider2D>(),false);
        if( playerNumber==2 && other.collider != coll2 && coll2 != null)
           Physics2D.IgnoreCollision(coll2,gameObject.GetComponent<Collider2D>(),false);
        
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
        if(other.gameObject.CompareTag("little")) SetLittleBool();
        coreMechanic.ApplyMechanic();
    }

    private void SetLittleBool()
    {
        if (playerNumber == 1) player1Little = true;
        if (playerNumber == 2) player2Little = true;
    }

    private void CollectStone(Collision2D other)
    {
        if (other.gameObject.CompareTag("touch"))
            hasTouch = true;
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
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>(); 
        if (_animator) _animator.SetBool(Wait1, false);
        _enterLoadLevel = false;
        _onDiamond = false;
        if (GetComponent<changeSize>() && GetComponent<changeSize>().GETLittle())
        {
            GetComponent<changeSize>().ApplyMechanic();
        }
        switch (scene.name) 
        {
            case "Tutorial2":
                gameManager.SetPosPlayer1(_pos1Tutorial2);
                gameManager.SetPosPlayer2(_pos2Tutorial2);
                break;
            case "Level1":
                gameManager.SetPosPlayer1(_pos1Level1);
                gameManager.SetPosPlayer2(_pos2Level1);
                break;
            case "Level2":
                if (playerNumber == 2)
                {
                    ResetPlayer2();
                }
                _animator.SetTrigger("hello");
                Legs.Pass = false;
                gameManager.SetPosPlayer1(_pos1Level2);
                gameManager.SetPosPlayer2(_pos2Level2);
                break;
            case "Level3":
                gameManager.SetPosPlayer1(_pos1Level3);
                gameManager.SetPosPlayer2(_pos2Level3);
                var light1 = transform.GetChild(transform.childCount-1);
                if (light1.CompareTag("light1")&& playerNumber==1)
                {
                    Destroy(light1.gameObject);
                    print("light1");
                }
                var light2 = transform.GetChild(transform.childCount-1);
                if (light2.CompareTag("light2")&&playerNumber==2)
                {
                    Destroy(light2.gameObject);
                    print("light2");
                }
                // SetPlayersLight();
                SetShrinkPower();
                break;
        }
    }

    private void ResetPlayer2()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 4;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Transform>().parent = null;
        GetComponent<Transform>().eulerAngles = Vector3.zero;
    }

    private void SetShrinkPower()
    {
        if (player2Little)
        {
            ShrinkManager.setRightShrink();
        }

        else if (player1Little)
        {
            ShrinkManager.SetLeftShrink();
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
    

    private void SetPlayersLight()
    {
        if (playerNumber == 1)
        {
            _light1 = GameObject.FindGameObjectWithTag("light1");
            _light1.transform.parent = transform;
        }

        if (playerNumber == 2)
        {
            _light2 = GameObject.FindGameObjectWithTag("light2");
            _light2.transform.parent = transform;
        }
            
    }
    
}

