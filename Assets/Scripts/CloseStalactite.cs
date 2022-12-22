using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStalactite : MonoBehaviour
{
    [SerializeField] private float playerPos;
    [SerializeField] private Vector3 target;
    [SerializeField] private Transform player;
    private float DampingTime = 1f;
    private Vector3 _velocity = Vector3.zero;
    private bool _reverse = false;

    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        target = _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x<= playerPos)  MoveStalactite();
    }

    private void MoveStalactite()
    {
        target = _reverse ? _startPos : target;
        _reverse = !_reverse;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, DampingTime);
    }
    
    
}
