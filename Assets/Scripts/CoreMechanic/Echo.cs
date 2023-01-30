using System;
using System.Collections;
using UnityEngine;

namespace CoreMechanic
{
    public class Echo : MonoBehaviour, ICoreMechanic
    {
        private static readonly int ApplyEcho = Animator.StringToHash("applyEcho");
        private LayerMask echoLayer;

        public void SetLayer(LayerMask layerMask)
        {
            echoLayer = layerMask;
        }
        public void ApplyMechanic()
        {
            GetComponent<Animator>().SetTrigger("echo");
            Collider2D overlapCircle = Physics2D.OverlapCircle(transform.position, 0.5f,echoLayer);
           
            if (overlapCircle && overlapCircle.gameObject.layer == 3)
            {
                overlapCircle.gameObject.GetComponentInParent<Animator>().SetTrigger(ApplyEcho);
                if (overlapCircle.gameObject.name == "flyCollider")
                    StartCoroutine(WaitToOpenWebs(overlapCircle));
            }
        }

        IEnumerator WaitToOpenWebs(Collider2D overlapCircle)
        {
            yield return new WaitForSeconds(4f);
            overlapCircle.gameObject.GetComponentsInParent<Collider2D>()[1].enabled = false;
        }

    }
}