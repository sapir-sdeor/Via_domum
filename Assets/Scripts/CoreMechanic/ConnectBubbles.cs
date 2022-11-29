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
        [SerializeField] private SpriteMask spriteMask;
        
        public void SetCollider(Collider2D colliderToRemove)
        {
            this.colliderToRemove = colliderToRemove;
        }     
        public void SetSpriteMask(SpriteMask spriteMask)
        {
            this.spriteMask = spriteMask;
        }  
        public void ApplyMechanic()
        {
            if (!colliderToRemove || !spriteMask) return;
            colliderToRemove.isTrigger = true;
            spriteMask.enabled = true;
        }

    }
}