
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
    private static readonly int Level2 = Animator.StringToHash("level2");


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag("touch") && other.gameObject.GetComponent<Acting>().GetTouch())
        {
            return;
        }
        newPowerAnimator.gameObject.SetActive(true);
        auraAnimator = aura.GetComponent<Animator>();
        auraAnimator.gameObject.SetActive(true);
        auraAnimator.SetTrigger(FadeIn);
        if (other.gameObject.name == UIManager.PLAYER1)
        {
            StartCoroutine(waitBeforeDestroy1());
        }
        else if (other.gameObject.name == UIManager.PLAYER2)
        {
            StartCoroutine(waitBeforeDestroy2());
        }
    }
    


    IEnumerator waitBeforeDestroy1()
    {
        yield return new WaitForSeconds(TimeToWait);
        aura.SetActive(false);
        text.SetActive(false);
        auraAnimator.SetBool(FadeOut, true);
        newPowerAnimator.SetBool(Player1, true);
        
    }
    
    IEnumerator waitBeforeDestroy2()
    {
        yield return new WaitForSeconds(TimeToWait);
        aura.SetActive(false);
        text.SetActive(false);
        auraAnimator.SetBool(FadeOut,true);
        if (LevelManager.GETLevel() == 1)
        {
            newPowerAnimator.SetBool(Player2, true);
        }
        else
        {
            newPowerAnimator.SetBool(Level2,true);
        }
    }
}
