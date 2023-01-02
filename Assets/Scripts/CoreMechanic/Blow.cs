
using UnityEngine;

namespace CoreMechanic
{
    public class Blow : MonoBehaviour, ICoreMechanic
    {
        [SerializeField] private GameObject bubbleFly;

       
        public void ApplyMechanic()
        {
            bubbleFly = GameObject.FindGameObjectWithTag("bubbleFly");
            if (!bubbleFly || !GetComponent<Collider2D>().IsTouching(bubbleFly.GetComponent<Collider2D>())) return;
            foreach (var animator in bubbleFly.GetComponentsInChildren<Animator>())
            {
                animator.SetBool("startFly", true);    
            }
            bubbleFly.AddComponent<flyAnimal>();
        }

    }
}