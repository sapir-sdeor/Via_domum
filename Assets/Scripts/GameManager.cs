using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {
        //TODO: play gate animation?
        gate.GetComponent<Collider2D>().enabled = false;
        gate.GetComponent<SpriteRenderer>().enabled = false;
    }
}
