using UnityEngine;

namespace CoreMechanic
{
    public class Fly : MonoBehaviour, ICoreMechanic
    {
        private float _time;
        private bool _fly;
        private void Update()
        {
            if (_fly)
                _time += Time.deltaTime;
            if (_time > 2)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                _fly = false;
                _time = 0;
            }
        }

        public void ApplyMechanic()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            _fly = true;
        }
    }
}