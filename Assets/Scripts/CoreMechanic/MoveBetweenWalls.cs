using UnityEngine;

namespace CoreMechanic
{
    public class MoveBetweenWalls : MonoBehaviour, ICoreMechanic
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