using UnityEngine;

namespace CoreMechanic
{
    public class Touch :  MonoBehaviour, ICoreMechanic
    {
        private static readonly int Touch1 = Animator.StringToHash("touch");

        public void ApplyMechanic()
        {
            GetComponent<Animator>().SetTrigger(Touch1);
            touchAct[] touchGameObjects = FindObjectsOfType<touchAct>();
            foreach (var obj in touchGameObjects)
            {
                if (GetComponent<Collider2D>().IsTouching(obj.GetComponent<Collider2D>()))
                {
                    obj.TouchFactory();
                }
            }
        }
    }
}