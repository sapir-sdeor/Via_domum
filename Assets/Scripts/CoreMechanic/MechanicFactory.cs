using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CoreMechanic
{
    public class MechanicFactory : MonoBehaviour
    {
        public ICoreMechanic CreateMechanic(String name, Vector3 flyPos, Light2D[] light2D)
        {
            switch (name)
            {
                case "fly":
                    if (gameObject.GetComponent<Fly>()) return gameObject.GetComponent<Fly>();
                    gameObject.AddComponent<Fly>();
                    gameObject.GetComponent<Fly>().SetFlyPosition(flyPos);
                    return gameObject.GetComponent<Fly>();
                case "rope":
                    if (gameObject.GetComponent<Rope>()) return gameObject.GetComponent<Rope>();
                    gameObject.AddComponent<Rope>();
                    return gameObject.GetComponent<Rope>();
                case "light":
                    if (gameObject.GetComponent<Light>()) return gameObject.GetComponent<Light>();
                    gameObject.AddComponent<Light>();
                    gameObject.GetComponent<Light>().SetLightOff(light2D);
                    return gameObject.GetComponent<Light>();
                case "blowUp":
                    print("blow factory");
                    if (gameObject.GetComponent<Blow>()) return gameObject.GetComponent<Blow>();
                    gameObject.AddComponent<Blow>();
                    return gameObject.GetComponent<Blow>();
                case "little":
                    if (gameObject.GetComponent<changeSize>()) return gameObject.GetComponent<changeSize>();
                    gameObject.AddComponent<changeSize>();
                    return gameObject.GetComponent<changeSize>();
                case "touch":
                    if (gameObject.GetComponent<Touch>()) return gameObject.GetComponent<Touch>();
                    gameObject.AddComponent<Touch>();
                    return gameObject.GetComponent<Touch>();

            }
            return null;
        }
    }
}

/*case "connect":
                    if (gameObject.GetComponent<ConnectBubbles>()) return gameObject.GetComponent<ConnectBubbles>();
                    gameObject.AddComponent<ConnectBubbles>();
                    gameObject.GetComponent<ConnectBubbles>().SetCollider(collider2D);
                    gameObject.GetComponent<ConnectBubbles>().SetSprite(sprite, background);
                    return gameObject.GetComponent<ConnectBubbles>();*/
/*case "moveWalls":
    if (gameObject.GetComponent<MoveBetweenWalls>()) return gameObject.GetComponent<MoveBetweenWalls>();
    gameObject.AddComponent<MoveBetweenWalls>();
    gameObject.GetComponent<MoveBetweenWalls>().SetCollider(collider2D);
    return gameObject.GetComponent<MoveBetweenWalls>();*/