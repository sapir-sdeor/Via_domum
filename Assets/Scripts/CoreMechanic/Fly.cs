using System.Collections;
using UnityEngine;

namespace CoreMechanic
{
    public class Fly : MonoBehaviour, ICoreMechanic
    {
        private float _time;
        private bool _fly;
        private Vector3 _flyPos;
        private float speed = 1.5f;
        private GameObject particle;
        private GameObject flower;
        private static readonly int Explode = Animator.StringToHash("explode");

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
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                GetComponent<Collider2D>().enabled = true;
                GetComponent<Animator>().SetBool("fly", false);
                
                //stop particle system
                if (particle) particle.GetComponent<ParticleSystem>().Stop();
            }
            if (_fly)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _flyPos, step);
            }
           
        }

        public void ApplyMechanic()
        {
            //activate particle system
            particle = GameObject.FindWithTag("particle system");
            flower = GameObject.FindWithTag("flower");
            if (particle) particle.GetComponent<ParticleSystem>().Play();
            if (flower) flower.GetComponent<Animator>().SetBool(Explode, true);
            StartCoroutine(StartAnimation());
        }

        IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(2);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool("fly", true);
            _fly = true;
        }
        
    }
}