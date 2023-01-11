using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingMusroom : MonoBehaviour
{
    private bool _move;

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
        if (!_move) _move = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
