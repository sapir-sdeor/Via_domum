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
        [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject background;
        
        public void SetCollider(Collider2D colliderToRemove)
        {
            this.colliderToRemove = colliderToRemove;
        }     
        public void SetSprite(Sprite sprite, GameObject gameObject)
        {
            this.sprite = sprite;
            background = gameObject;
        }  
        public void ApplyMechanic()
        {
            if (!colliderToRemove || !sprite || !background) return;
            colliderToRemove.isTrigger = true;
            background.GetComponent<SpriteRenderer>().sprite = sprite;
        }

    }
}