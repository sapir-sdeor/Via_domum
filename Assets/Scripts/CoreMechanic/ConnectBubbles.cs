
using UnityEngine;

namespace CoreMechanic
{
    public class ConnectBubbles : MonoBehaviour, ICoreMechanic
    {
        //[SerializeField] private Collider2D colliderToRemove;
     //   [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject background;
        
        public void SetInputs(GameObject background)
        {
            this.background = background;
        }     
        public void SetSprite(Sprite sprite, GameObject gameObject)
        {
     //       this.sprite = sprite;
            background = gameObject;
        }  
        public void ApplyMechanic()
        {
            if (!background) return;
          //  colliderToRemove.isTrigger = true;
            background.GetComponent<Collider2D>().isTrigger = true;
            background.GetComponent<Animator>().SetTrigger("connect");
           // background.GetComponent<SpriteRenderer>().sprite = sprite;
        }

    }
}