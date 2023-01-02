
using UnityEngine;

public class flyAnimal : MonoBehaviour
{
    private Animator _animator;
    private static readonly int StartFly = Animator.StringToHash("startFly");
    private GameManager _gameManager;
    private float _time;
    private bool _startFly;
    [SerializeField] private GameObject stoneToFall;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (_startFly) _time += Time.deltaTime;
        if (_time > 1.7f && !_animator.GetCurrentAnimatorStateInfo(0).IsName("flyAnim1"))
        {
            _gameManager.FallDiamond(stoneToFall);
            _startFly = false;
            _time = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger(StartFly);
            _startFly = true;
        }
    }
}
