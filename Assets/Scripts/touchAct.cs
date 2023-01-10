using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using Unity.VisualScripting;
using UnityEngine;

public class touchAct : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private static readonly int Connect = Animator.StringToHash("connect");

    public void TouchFactory()
    {
        switch (tag)
        {
            case "connect":
                ConnectBubbles();
                break;
            case "rope":
                OpenRope();
                break;
        }
    }

    private void ConnectBubbles()
    {
        if (!background) return;
        background.GetComponent<Collider2D>().isTrigger = true;
        background.GetComponent<Animator>().SetTrigger(Connect);
    }

    private void OpenRope()
    {
        //TODO: apply rope
    }
}
