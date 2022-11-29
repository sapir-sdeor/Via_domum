using UnityEngine;

namespace CoreMechanic
{
    public class Fly : MonoBehaviour, ICoreMechanic
    {
        private float _time;
        private bool _fly;
        private Vector3 _flyPos;
        private float speed = 6f;

        public void SetFlyPosition(Vector3 flyPosition)
        {
            _flyPos = flyPosition;
        }
        private void Update()
        {
            if (Vector3.Distance(transform.position, _flyPos) < 0.3f)
            {
                _fly = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            if (_fly)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _flyPos, step);
            }
           
        }

        public void ApplyMechanic()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            _fly = true;
        }
    }
}