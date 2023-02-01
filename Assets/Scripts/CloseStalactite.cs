
using UnityEngine;

public class CloseStalactite : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private bool downTunnel;
    private Transform player;
    private Vector3 target;
    private float DampingTime = 1f;
    private Vector3 _velocity = Vector3.zero;
    private bool _reverse = false;
    private Vector3 _startPos;
    private float _playerPos;


    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        /*if ((downTunnel && ButtonManger.PlayerNumberInDownTunnel == 1) || 
            (!downTunnel && ButtonManger.PlayerNumberInDownTunnel == 2))*/
            /*player = _gameManager.GETPlayer1().gameObject.transform;
        else
            player = _gameManager.GETPlayer2().gameObject.transform;*/
        _playerPos = downTunnel ? 0.66f : -1.01f;
        print(player.gameObject.GetComponent<Acting>());
        target =_startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print("pos: " + player.position.x);
        if (downTunnel && player.position.x <= _playerPos)  MoveStalactite();
        else if (!downTunnel && player.position.x >= _playerPos && player.position.y > 0.27f)  MoveStalactite();
    }

    private void MoveStalactite()
    {
        target = _reverse ? _startPos : targetPos.position;
        _reverse = !_reverse;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, DampingTime);
    }
    
    
}
