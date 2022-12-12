using UnityEngine;

namespace CoreMechanic
{
    public class Fly : MonoBehaviour, ICoreMechanic
    {
        private float _time;
        private bool _fly;
        private Vector3 _flyPos;
        private float speed = 1.5f;

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
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                GetComponent<Collider2D>().enabled = true;
                GetComponent<Animator>().SetBool("fly", false);
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
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool("fly", true);
            _fly = true;
        }
    }
}