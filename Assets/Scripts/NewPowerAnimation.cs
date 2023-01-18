
using System.Collections;
using UnityEngine;

public class NewPowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator newPowerAnimator;
    [SerializeField] private GameObject aura, text;
    [SerializeField] private float TimeToWait=0.7f;
    private static readonly int Player1 = Animator.StringToHash("player1");
    private static readonly int Player2 = Animator.StringToHash("player2");


    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(waitBeforeDestroy());
        if (other.gameObject.name == UIManager.PLAYER1)
        {
            newPowerAnimator.SetBool(Player1, true);
        }
        else if (other.gameObject.name == UIManager.PLAYER2)
        {
            newPowerAnimator.SetBool(Player2, true);
        }
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(TimeToWait);
        aura.SetActive(false);
        text.SetActive(false);
    }
}
