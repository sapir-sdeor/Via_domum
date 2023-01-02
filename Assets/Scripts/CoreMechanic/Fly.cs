using System;
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
        private GameObject[] _particle;
        private GameObject[] _flower;
        private int _indexFlower;
        private bool _explodeFlower;
        private static readonly int Explode = Animator.StringToHash("explode");
        private static readonly int Fly1 = Animator.StringToHash("fly");

        private void Awake()
        {
            _particle = GameObject.FindGameObjectsWithTag("particle system");
            print(_particle);
            _flower = GameObject.FindGameObjectsWithTag("flower");
        }

        public void SetFlyPosition(Vector3 flyPosition)
        {
            _flyPos = flyPosition;
        }
        private void Update()
        {
            if (_explodeFlower) _time += Time.deltaTime;
            if (_time > 4)
            {
                _explodeFlower = false;
                _time = 0;
                if (_indexFlower < _particle.Length)
                {
                    _particle[_indexFlower].GetComponent<ParticleSystem>().Stop();
                    _indexFlower++;
                }
            }
            if (Vector3.Distance(transform.position, _flyPos) < 0.3f)
            {
                _fly = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                GetComponent<Collider2D>().enabled = true;
                GetComponent<Animator>().SetBool(Fly1, false);
            }
            if (_fly)
            {
                var step = speed * Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                transform.position = Vector3.MoveTowards(transform.position, _flyPos, step);
            }
           
        }

        public void ApplyMechanic()
        {
            //activate particle system
            if (_indexFlower < _particle.Length) _particle[_indexFlower].GetComponent<ParticleSystem>().Play();
            if (_indexFlower < _flower.Length)
            {
                _flower[_indexFlower].GetComponent<Animator>().SetBool(Explode, true);
                _explodeFlower = true;
                if (_flower[_indexFlower].name == "Main Flower")
                {
                    StartFlying();
                }
            }
        }

        IEnumerator StartAnimation()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(2);
            _fly = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool(Fly1, true);
        }

        private void StartFlying()
        {
            _fly = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool(Fly1, true);
        }
        

        public bool GETFly()
        {
            return _fly;
        }
        
    }
}