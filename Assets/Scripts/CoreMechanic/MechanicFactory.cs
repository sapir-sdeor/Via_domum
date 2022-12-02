using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CoreMechanic
{
    public class MechanicFactory : MonoBehaviour
    {
        public ICoreMechanic CreateMechanic(String name, Collider2D collider2D, Vector3 flyPos,
                                                    Sprite sprite, GameObject background, Light2D[] light2D)
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
                    gameObject.GetComponent<ConnectBubbles>().SetSprite(sprite, background);
                    return gameObject.GetComponent<ConnectBubbles>();
                case "moveWalls":
                    gameObject.AddComponent<MoveBetweenWalls>();
                    gameObject.GetComponent<MoveBetweenWalls>().SetCollider(collider2D);
                    return gameObject.GetComponent<MoveBetweenWalls>();
                case "light":
                    gameObject.AddComponent<Light>();
                    gameObject.GetComponent<Light>().SetLight(light2D);
                    return gameObject.AddComponent<Light>();
            }
            return null;
        }
    }
}