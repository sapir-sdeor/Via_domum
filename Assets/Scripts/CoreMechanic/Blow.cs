using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace CoreMechanic
{
    public class Blow : MonoBehaviour, ICoreMechanic
    {
        [SerializeField] private GameObject bubbleFly;

        public void SetBubbleFly(GameObject bubbleFly)
        {
            this.bubbleFly = bubbleFly;
        }
        public void ApplyMechanic()
        {
            if (!bubbleFly || !GetComponent<Collider2D>().IsTouching(bubbleFly.GetComponent<Collider2D>())) return;
            //TODO: animation of bubble fly
            bubbleFly.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().FallDiamond(null);
        }

    }
}