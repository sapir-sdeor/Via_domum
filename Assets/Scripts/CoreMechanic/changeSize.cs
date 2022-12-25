using System;
using UnityEngine;

namespace CoreMechanic
{
    public class changeSize : MonoBehaviour, ICoreMechanic
    {
        private bool _little;
        private Vector3 _regularScale;

        private void Awake()
        {
            _regularScale = transform.localScale;
        }

        public void ApplyMechanic()
        {
            if (_little)
            {
                transform.localScale = _regularScale;
                if (GetComponent<Acting>().IsFacingRight() && _regularScale.x > 0 || 
                    !GetComponent<Acting>().IsFacingRight() && _regularScale.x < 0)
                {
                    GetComponent<Acting>().Flip();
                }
                _little = false;
            }
            else
            {
                var localScale = transform.localScale;
                var newX = localScale.x - 0.2f;
                if (localScale.x < 0) newX = localScale.x + 0.2f;
                transform.localScale = new Vector3(newX, localScale.y - 0.2f,
                    localScale.z);
                _little = true;
            }
        }
    }
}