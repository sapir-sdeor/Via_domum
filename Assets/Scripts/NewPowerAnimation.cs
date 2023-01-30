
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NewPowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator newPowerAnimator;
    private Animator auraAnimator;
    [SerializeField] private GameObject aura, text;
    [SerializeField] private float TimeToWait=0.7f;
    private static readonly int Player1 = Animator.StringToHash("player1");
    private static readonly int Player2 = Animator.StringToHash("player2");
    private static readonly int FadeIn = Animator.StringToHash("fadeIn");
    private static readonly int FadeOut = Animator.StringToHash("fadeOut");



    private void OnCollisionEnter2D(Collision2D other)
    {
        newPowerAnimator.gameObject.SetActive(true);
        auraAnimator = aura.GetComponent<Animator>();
        auraAnimator.gameObject.SetActive(true);
        auraAnimator.SetTrigger(FadeIn);
        if (other.gameObject.name == UIManager.PLAYER1)
        {
            auraAnimator.SetBool(FadeOut, true);
            newPowerAnimator.SetBool(Player1, true);
            StartCoroutine(waitBeforeDestroy1());
        }
        else if (other.gameObject.name == UIManager.PLAYER2)
        {
            auraAnimator.SetBool(FadeOut,true);
            newPowerAnimator.SetBool(Player2, true);
            StartCoroutine(waitBeforeDestroy2());
            // auraAnimator.SetBool(FadeIn,false);
        }
    }

    private void Update()
    {
        
    }


    IEnumerator waitBeforeDestroy1()
    {
        // yield return new WaitForSeconds(TimeToWait);
        // float TimeToWait = 3f;
        var elapsedTime = 0f;
        // while (elapsedTime < TimeToWait)
        // {
        //     elapsedTime += Time.deltaTime;
        //     yield return null;
        // }
        // aura.SetActive(false);
        // text.SetActive(false);
        auraAnimator.SetBool(FadeOut, true);
        newPowerAnimator.SetBool(Player1, true);
        return null;
    }
    
    IEnumerator waitBeforeDestroy2()
    {
        // yield return new WaitForSeconds(TimeToWait);
        // aura.SetActive(false);
        // text.SetActive(false);
        auraAnimator.SetBool(FadeOut,true);
        newPowerAnimator.SetBool(Player2, true);
        return null;
    }
}
