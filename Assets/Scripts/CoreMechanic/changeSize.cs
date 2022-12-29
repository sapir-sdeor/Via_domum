
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
                
                if (_regularScale.x < 0 && transform.localScale.x > 0 || 
                    _regularScale.x > 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-_regularScale.x, _regularScale.y, _regularScale.z);
                }
                else transform.localScale = _regularScale;
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