using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject light1, light2;

    [SerializeField] private Vector3 pos1, pos2;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Acting.SetLightPlayer1( light1, pos1);
        Acting.SetLightPlayer2( light2, pos2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
