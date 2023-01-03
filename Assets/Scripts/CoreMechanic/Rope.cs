using UnityEngine;

namespace CoreMechanic
{
    public class Rope :  MonoBehaviour, ICoreMechanic
    {
        private GameObject _rope;
        public void ApplyMechanic()
        {
            if(!_rope) _rope = transform.GetChild(transform.childCount - 1).gameObject;
            if (_rope.activeSelf)
                RemoveRope();
            else
                AddRope();

        }

        private void AddRope()
        {
            _rope.SetActive(true);
        }

        private void RemoveRope()
        {
            _rope.SetActive(false);
        }
    }
}
