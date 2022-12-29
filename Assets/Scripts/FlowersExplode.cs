
using UnityEngine;

public class FlowersExplode : MonoBehaviour
{

    //private bool changeColor;

    private Animator _animator;
    private bool _collide;
    private static readonly int Explode = Animator.StringToHash("explode");
    private static readonly int Appear = Animator.StringToHash("appear");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetBool(Explode, true);
        if (transform.childCount > 0 && !_collide)
        {
            Transform child = transform.GetChild(0);
            child.GetComponent<Animator>().SetBool(Appear, true);
            _collide = true;
        }
    }
     /*changeColor = !changeColor;
           if(changeColor) gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
           else gameObject.GetComponent<SpriteRenderer>().color = Color.white;*/
}
