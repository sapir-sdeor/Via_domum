using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gate;

    public void OpenGate()
    {
        //TODO: play gate animation?
        gate.GetComponent<Collider2D>().enabled = false;
        gate.GetComponent<SpriteRenderer>().enabled = false;
    }
}
