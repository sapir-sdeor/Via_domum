using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingMusroom : MonoBehaviour
{
    private bool _move;
    public bool onMushroom;

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
        onMushroom = true;
        if (!_move) _move = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        onMushroom = false;
        other.transform.SetParent(null);
    }
}
