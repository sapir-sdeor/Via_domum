using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace CoreMechanic
{
    public class ConnectBubbles : MonoBehaviour, ICoreMechanic
    {
        [SerializeField] private Collider2D colliderToRemove;

        public void SetCollider(Collider2D collider2D)
        {
            colliderToRemove = collider2D;
        }       
        public void ApplyMechanic()
        {
            if (!colliderToRemove) return;
            colliderToRemove.isTrigger = true;
        }

    }
}