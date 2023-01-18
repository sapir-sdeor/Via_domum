using System;
using System.Collections;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private bool _fly;
    private Vector3 _flyPos;
    private float speed = 1.5f;
    private static readonly int Fly1 = Animator.StringToHash("fly");
    

    private void Update()
    {
        if (Vector3.Distance(transform.position, _flyPos) < 0.3f && _fly)
        {
            _fly = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Animator>().SetBool(Fly1, false);
        }
        if (_fly)
        {
            var step = speed * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, _flyPos, step);
        }
    }
    
    public void StartFlying(Vector3 flyPos)
    {
        _flyPos = flyPos;
        _fly = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool(Fly1, true);
    }
    

    public bool GETFly()
    {
        return _fly;
    }
    
}
