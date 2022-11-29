using System;
using UnityEngine;

namespace CoreMechanic
{
    public class MechanicFactory : MonoBehaviour
    {
        public ICoreMechanic CreateMechanic(String name, Collider2D collider2D, Vector3 flyPos,
                                                    SpriteMask spriteMask)
        {
            switch (name)
            {
                case "fly":
                    gameObject.AddComponent<Fly>();
                    gameObject.GetComponent<Fly>().SetFlyPosition(flyPos);
                    return gameObject.GetComponent<Fly>();
                case "connect":
                    gameObject.AddComponent<ConnectBubbles>();
                    gameObject.GetComponent<ConnectBubbles>().SetCollider(collider2D);
                    gameObject.GetComponent<ConnectBubbles>().SetSpriteMask(spriteMask);
                    return gameObject.GetComponent<ConnectBubbles>();
                case "moveWalls":
                    gameObject.AddComponent<MoveBetweenWalls>();
                    gameObject.GetComponent<MoveBetweenWalls>().SetCollider(collider2D);
                    return gameObject.GetComponent<MoveBetweenWalls>();
            }
            return null;
        }
    }
}