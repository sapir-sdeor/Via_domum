using System;
using UnityEngine;

namespace CoreMechanic
{
    public class MechanicFactory : MonoBehaviour
    {
        public ICoreMechanic CreateMechanic(String name, Collider2D collider2D)
        {
            switch (name)
            {
                case "fly":
                    return gameObject.AddComponent<Fly>();
                case "connect":
                    gameObject.AddComponent<ConnectBubbles>();
                    gameObject.GetComponent<ConnectBubbles>().SetCollider(collider2D);
                    return gameObject.GetComponent<ConnectBubbles>();
            }
            return null;
        }
    }
}