using UnityEngine;

namespace CoreMechanic
{
    public class Touch :  MonoBehaviour, ICoreMechanic
    {
        private static readonly int Touch1 = Animator.StringToHash("touch");

        public void ApplyMechanic()
        {
            GetComponent<Animator>().SetTrigger(Touch1);
        }
    }
}