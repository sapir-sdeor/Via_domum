using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreMechanic
{
    public class DestroyBubble : MonoBehaviour, CoreMechanic
    {
        [SerializeField] private Collider2D _colliderToRemove;


        public void SetCollider(Collider2D collider)
        {
            _colliderToRemove = collider;
            ConnectBubbles();
        }       
        public void ApplyMechanic()
        {
            
        }

        private void BlowUp()
        {
            
        }

        private void ConnectBubbles()
        {
            _colliderToRemove.isTrigger = true;
        }

        /*private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("blowUp"))
                BlowUp();
        }*/
    }
}