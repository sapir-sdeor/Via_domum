
using UnityEngine;

public class OilLamp : MonoBehaviour
{
    [SerializeField] private Vector3 newScale;
    [SerializeField] private float lampPos = 0.1f;
    [SerializeField] private int playerNumber;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == UIManager.PLAYER1||
            other.gameObject.name == UIManager.PLAYER2)
        {
            var trans = other.gameObject.transform;
            var pos = trans.position;
            GameObject oilLamp = Instantiate(gameObject,pos+Vector3.left*lampPos,
                Quaternion.identity,trans.transform); 
            oilLamp.GetComponent<Collider2D>().enabled = false;
            oilLamp.transform.localScale = newScale;
            Destroy(gameObject);
        }
    }
}
