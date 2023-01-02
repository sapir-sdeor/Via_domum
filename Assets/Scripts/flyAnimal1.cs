using System;
using UnityEngine;

public class flyAnimal1 : MonoBehaviour
{
    private float _time;
    private bool _startFly = true;
    private GameObject spider;

    private void Start()
    {
        spider = GameObject.FindGameObjectWithTag("spider");
    }

    void Update()
    {
        if (_startFly) _time += Time.deltaTime;
        // && !_animator.GetCurrentAnimatorStateInfo(0).IsName("flyAnim1")
        if (_time > 1.7f)
        {
            spider.GetComponent<Animator>().SetBool("moveSpider", true);
            _startFly = false;
            _time = 0;
        }
    }
}
