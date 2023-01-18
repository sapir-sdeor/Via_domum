using System;
using System.Collections;
using UnityEngine;

namespace CoreMechanic
{
    public class Echo : MonoBehaviour, ICoreMechanic
    {
        private static readonly int ApplyEcho = Animator.StringToHash("applyEcho");

        public void ApplyMechanic()
        {
            GetComponent<Animator>().SetTrigger("echo");
            Collider2D collider = Physics2D.OverlapCircle
                (transform.position, 0.2f, LayerMask.NameToLayer("Echo"));
            if (collider)
            {
                collider.gameObject.GetComponent<Animator>().SetTrigger(ApplyEcho);
            }
        }

    }
}