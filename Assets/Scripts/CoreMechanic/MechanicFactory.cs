using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CoreMechanic
{
    public class MechanicFactory : MonoBehaviour
    {
        public ICoreMechanic CreateMechanic(String name, Light2D[] light2D, LayerMask echoLayer)
        {
            switch (name)
            {
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
                case "echo":
                    if (gameObject.GetComponent<Echo>()) return gameObject.GetComponent<Echo>();
                    gameObject.AddComponent<Echo>();
                    gameObject.GetComponent<Echo>().SetLayer(echoLayer);
                    return gameObject.GetComponent<Echo>();

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