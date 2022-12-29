
using CoreMechanic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Acting _player1;
    private Acting _player2;
    private GameManager _gameManager;
    private float _time;
    private float _cameraZoom = 2.5f;
    private float _cameraFirstZoom;
    private Vector3 _target;
    private Vector3 _startPos;
    
    void Start()
    {
        _startPos = transform.position;
        _gameManager = FindObjectOfType<GameManager>();
        _player1 = _gameManager.GETPlayer1();
        _player2 = _gameManager.GETPlayer2();
        _cameraFirstZoom = GetComponent<Camera>().orthographicSize;
    }

    
    void Update()
    {
        if (_player2.GetComponent<Fly>() && _player2.GetComponent<Fly>().GETFly())
        {
            FlyPowerZoom();
        }
        else if (transform.position != _startPos)
        {
            GetComponent<Camera>().orthographicSize = _cameraFirstZoom;
            transform.position = _startPos;
        }
    }

    public void FlyPowerZoom()
    {
        _time += Time.deltaTime;
        _target = _time <= 0.4 ? _player1.transform.position : _player2.transform.position;
        transform.position = _target + new Vector3(1,1,transform.position.z);
        GetComponent<Camera>().orthographicSize = _cameraZoom;
    } 
}
